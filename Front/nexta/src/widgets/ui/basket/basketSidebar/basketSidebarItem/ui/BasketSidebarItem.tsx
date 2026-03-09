import React, { useState } from "react"
import { useNotifications } from "../../../../../../shared/contexts/notifications/NotificationsContext";
import CheckSvg from "../../../../svg/CheckSvg/CheckSvg";
import TrashSvg from "../../../../svg/TrashSvg/TrashSvg";
import { useNavigate } from "react-router-dom";
import { BasketItem } from "../../../../../../entities/basket/basketItem";
import authStore from "../../../../../../shared/stores/auth/authStore";
import styles from './BasketSidebarItem.module.css';
import { observer } from "mobx-react";
import { useRemoveProductFromBasket } from "../../../../../../features/basket/removeProductFromBasket/useRemoveProductFromBasket";
import { useUpdateProductInBasket } from "../../../../../../features/basket/updateProductInBasket/useUpdateProductInBasket";

export const BasketSidebarItem:React.FC<{basketItem: BasketItem}> = observer(({basketItem}) => {
    const [count, setCount] = useState(basketItem.count);
    const [legacyCount, setLegacyCount] = useState(basketItem.count);
    const navigate = useNavigate();
    const { addNotification } = useNotifications();
    const { removeProductFromBasket } = useRemoveProductFromBasket();
    const { updateBasketProduct } = useUpdateProductInBasket();

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const raw = parseInt(e.target.value, 10);
        const safe = !isNaN(raw) && raw >= 1 ? raw : 1;

        setCount(safe);
        basketItem.count = safe;
    };

    const goToProductPage = (id:string) => {
        navigate(`/Product/${id}`);
    }

    const increment = () => {
        setCount(count => count + 1);
    };

    const decrement = () => {
        setCount(count => Math.max(1, count - 1));
    };

    const handleUpdateProduct = async(productId:string, count:number) => {
        const userId = authStore?.user?.id ?? '';
        await updateBasketProduct(userId, productId, count);
        setLegacyCount(count);
    }

    const handleDeleteProductFromBasket = async() => {
        
        const userId = authStore?.user?.id ?? '';
        const productId = basketItem.productId;

        await removeProductFromBasket(productId, userId);
    };

    return <div className={styles.container}>
        <div className={styles.header} onClick={() => goToProductPage(basketItem.productId)}>
            <div className={styles.headerItem}>
                {basketItem?.product?.name}
            </div>
            <div className={styles.headerItem}>
                {basketItem?.product?.article}
            </div>
        </div>
        <div className={styles.body}>
            <div className={styles.description}>
                {basketItem?.product?.description}
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
                    {count * (basketItem?.product?.newPrice ?? 0)} руб.
                </span>
                {(basketItem?.product?.oldPrice !== undefined && basketItem?.product?.oldPrice != 0) &&
                    <span className={styles.oldPrice}>
                        {count * basketItem?.product?.oldPrice} руб.
                    </span>
                }
            </div>
            <div>
                {(count != legacyCount) &&
                    <button className={styles.updateBasketBtn}
                    data-tooltip='Подтвердить изменения' 
                    onClick={() => handleUpdateProduct(basketItem?.productId, count)}>
                        <CheckSvg />
                    </button>
                }
                <button className={styles.removeBasketBtn} data-tooltip='Удалить из корзины' onClick={handleDeleteProductFromBasket}>
                    <TrashSvg />
                </button>
            </div>
        </div>
    </div>
});