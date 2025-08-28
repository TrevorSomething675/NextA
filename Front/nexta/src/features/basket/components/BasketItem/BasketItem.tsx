import { useState } from 'react';
import styles from './BasketItem.module.css';
import { observer } from 'mobx-react';
import { useNavigate } from 'react-router-dom';
import basket from '../../../../stores/basket';
import BasketService from '../../../../services/BasketService';
import authStore from '../../../../stores/AuthStore/authStore';
import CheckSvg from '../../../../shared/svgs/CheckSvg/CheckSvg';
import TrashSvg from '../../../../shared/svgs/TrashSvg/TrashSvg';
import { useNotifications } from '../../../../shared/components/Notifications/Notifications';
import { BasketProduct, BasketProductStatus } from '../../../../models/BasketProduct';
import { UpdateBasketProductRequest } from '../../../../http/models/basketProduct/UpdateBasketProduct';
import { toJS } from 'mobx';

const BasketItem:React.FC<{product:BasketProduct}> = observer(({product}) => {
    const [count, setCount] = useState(product.count);
    const [legacyCount, setLegacyCount] = useState(product.count);
    const navigate = useNavigate();
    const {addNotification} = useNotifications();

    const statusLabels = {
        [BasketProductStatus.Unknown]: 'Неизвестный статус',
        [BasketProductStatus.Accepted]: 'Принят',
        [BasketProductStatus.AtWork]: 'В работе',
        [BasketProductStatus.Rejected]: 'Отказ',
        [BasketProductStatus.Waiting]: 'Ожидает'
    };

    const goToProductPage = () => {
        navigate(`/Products/${product.productId}`);
    };

    const handleDeleteProductFromBasket = async() =>{
        const userId = authStore?.user?.id ?? '';
        const productId = product.productId;
        console.warn(toJS(basket.items));

        const response = await BasketService.DeleteBasketProduct({userId: userId, productId: productId});
        if(response.success && response.status === 200){
            console.warn(productId);
            basket.deleteBasketProduct(productId);
        }
    };

    const handleUpdateProduct = async(productId:string, count:number) => {
        const request: UpdateBasketProductRequest = {
            userId: authStore?.user?.id ?? '',
            productId: productId,
            count: count
        }
        const response = await BasketService.UpdateBasketDetail(request);

        if(response.success == true && response.status === 200){
            basket.changeProductCount(response.data.productId, response.data.count);
            setLegacyCount(response.data.count);
            addNotification({
                header: 'Корзина обновлена',
                body: `Товар: ${product.name}. Изменения успешно внесены.`
            })
        }
    }

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const raw = parseInt(e.target.value, 10);
        const safe = !isNaN(raw) && raw >= 1 ? raw : 1;

        setCount(safe);
        product.count = safe;
    };

    const increment = () => {
        setCount(count => count + 1);
    };

    const decrement = () => {
        setCount(count => Math.max(1, count - 1));
    };
    
    return <tr className={styles.tr}>
        <td>
            <button onClick={goToProductPage} className={styles.button}>{product.name}</button>
        </td>
        <td>
            {product.article}
        </td>
        <td>{statusLabels[product.status]}</td>
        <td>{product !== undefined && product.deliveryDate}</td>
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
                {count * product.newPrice} руб.
            </span>
            {(product.oldPrice !== undefined && product.oldPrice != 0) &&
                <span className={styles.oldPrice}>
                    {count * product.oldPrice} руб.
                </span>
            }
        </td>
        <td className={styles.buttonsContainer}>
            {(count != legacyCount) &&
                <button className={styles.updateBasketBtn}
                data-tooltip='Подтвердить изменения' 
                onClick={() => handleUpdateProduct(product.productId, count)}>
                    <CheckSvg />
                </button>
            }
            <button className={styles.removeBasketBtn} data-tooltip='Удалить из корзины' onClick={handleDeleteProductFromBasket}>
                <TrashSvg />
            </button>
        </td>
    </tr>
});

export default BasketItem;