import { UserBasketProduct } from "../../../../../models/UserBasketProduct"
import React, { useState } from "react"
import styles from './BasketSidebarItem.module.css';
import { UpdateBasketProductRequest } from "../../../../../http/models/basketProduct/UpdateBasketProduct";
import authStore from "../../../../../stores/AuthStore/authStore";
import BasketService from "../../../../../services/BasketService";
import basket from "../../../../../stores/basket";
import { useNotifications } from "../../../../../shared/components/Notifications/Notifications";
import CheckSvg from "../../../../../shared/svgs/CheckSvg/CheckSvg";
import TrashSvg from "../../../../../shared/svgs/TrashSvg/TrashSvg";
import { useNavigate } from "react-router-dom";

export const BasketSidebarItem:React.FC<{product: UserBasketProduct}> = ({product}) => {
    const [count, setCount] = useState(product.count);
    const [legacyCount, setLegacyCount] = useState(product.count);
    const navigate = useNavigate();
    const {addNotification} = useNotifications();

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

    const handleUpdateProduct = async(productId:string, count:number) => {
        const request: UpdateBasketProductRequest = {
            userId: authStore?.user?.id ?? '',
            productId: productId,
            count: count,
            deliveryDate: product.deliveryDate
        }
        const response = await BasketService.UpdateBasketProduct(request);

        if(response.success == true && response.status === 200){
            basket.changeProductCount(response.data.productId, response.data.count);
            setLegacyCount(response.data.count);
            addNotification({
                header: 'Корзина обновлена',
                body: `Товар: ${product.name}. Изменения успешно внесены.`
            })
        }
    }

    const handleDeleteProductFromBasket = async() =>{
        const userId = authStore?.user?.id ?? '';
        const productId = product.productId;

        const response = await BasketService.DeleteBasketProduct({userId: userId, productId: productId});
        if(response.success && response.status === 200){
            basket.deleteBasketProduct(productId);
        }
    };

    return <div className={styles.container}>
        <div className={styles.header}>
            <div className={styles.headerItem}>
                {product.name}
            </div>
            <div className={styles.headerItem}>
                {product.article}
            </div>
        </div>
        <div className={styles.body}>
            <div className={styles.description}>
                {product.description}
            </div>
        </div>
        <div className={styles.footer}>
            <div>
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
                <span className={styles.newPrice}>
                    {count * product.newPrice} руб.
                </span>
                {(product.oldPrice !== undefined && product.oldPrice != 0) &&
                    <span className={styles.oldPrice}>
                        {count * product.oldPrice} руб.
                    </span>
                }
            </div>
            <div>
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
            </div>
        </div>
    </div>
}