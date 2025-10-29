import { useState } from 'react';
import { observer } from 'mobx-react';
import { useNavigate } from 'react-router-dom';
import CheckSvg from '../../svg/CheckSvg/CheckSvg';
import TrashSvg from '../../svg/TrashSvg/TrashSvg';
import { useNotifications } from '../../../../sharedLegacy/components/Notifications/Notifications';
import authStore from '../../../../shared/stores/auth/authStore';
import basketStore from '../../../../shared/stores/basket/basketStore';
import { BasketItem } from '../../../../entities/basket/models/basketItem';
import { BasketApi } from '../../../../shared/http/basket/basketApi';
import styles from './BasketProductItem.module.css';

export const BasketProductItem:React.FC<{basketProduct:BasketItem}> = observer(({basketProduct}) => {
    const [count, setCount] = useState(basketProduct.count);
    const [legacyCount, setLegacyCount] = useState(basketProduct.count);
    const navigate = useNavigate();
    const {addNotification} = useNotifications();

    const goToProductPage = () => {
        navigate(`/Product/${basketProduct.productId}`);
    };

    const handleDeleteProductFromBasket = async() =>{
        const userId = authStore?.user?.id ?? '';
        const productId = basketProduct.productId;

        const response = await BasketApi.DeleteProductFromBasket(userId, productId);
        if(response.success && response.status === 200){
            basketStore.deleteBasketProduct(productId);
        }
    };

    const handleUpdateProduct = async(productId:string, count:number) => {
        const userId = authStore?.user?.id ?? ''

        const response = await BasketApi.Update(userId, productId, count);

        if(response.success == true && response.status === 200){
            basketStore.changeProductCount(response.data.productId, response.data.count);
            setLegacyCount(response.data.count);
            addNotification({
                header: 'Корзина обновлена',
                body: 'Изменения успешно внесены.'
            })
        }
    }

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const raw = parseInt(e.target.value, 10);
        const safe = !isNaN(raw) && raw >= 1 ? raw : 1;

        setCount(safe);
        basketProduct.count = safe;
    };

    const increment = () => {
        setCount(count => count + 1);
    };

    const decrement = () => {
        setCount(count => Math.max(1, count - 1));
    };
    
    return <tr className={styles.tr}>
        <td>
            <button onClick={goToProductPage} className={styles.button}>{basketProduct?.product?.name}</button>
        </td>
        <td>
            {basketProduct?.product?.article}
        </td>
        <td>
            <div className={styles.description}>
                {basketProduct?.product?.description}
            </div>
        </td>
        <td>
            <button type="button" className={styles.down} onClick={decrement}>◄</button>
                <input
                    value={count}
                    type="number"
                    name="quantity"
                    min="1"
                    max="10"
                    step="1"
                    className={styles.countInput}
                    onChange={handleInputChange}
                />
            <button type="button" className={styles.up} onClick={increment}>►</button>
        </td>
        <td>
            x
        </td>
        <td>
            <span className={styles.newPrice}>
                {count * (basketProduct?.product?.newPrice ?? 0)} руб.
            </span>
            {(basketProduct?.product?.oldPrice !== undefined && basketProduct?.product?.oldPrice != 0) &&
                <span className={styles.oldPrice}>
                    {count * (basketProduct?.product?.oldPrice ?? 0)} руб.
                </span>
            }
        </td>
        <td className={styles.buttonsContainer}>
            {(count != legacyCount) &&
                <button className={styles.updateBasketBtn}
                data-tooltip='Подтвердить изменения' 
                onClick={() => handleUpdateProduct(basketProduct.productId, count)}>
                    <CheckSvg />
                </button>
            }
            <button className={styles.removeBasketBtn} data-tooltip='Удалить из корзины' onClick={handleDeleteProductFromBasket}>
                <TrashSvg />
            </button>
        </td>
    </tr>
});