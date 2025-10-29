import { observer } from "mobx-react";
import { useEffect, useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useNotifications } from "../../../../../sharedLegacy/components/Notifications/Notifications";
import basketStore from "../../../../../shared/stores/basket/basketStore";
import authStore from "../../../../../shared/stores/auth/authStore";
import { OrderApi } from "../../../../../shared/http/order/orderApi";
import styles from './BasketSidebar.module.css';
import { OrderItem } from "../../../../../entities/order/models/orderItem";
import { Button } from "../../../../../shared/ui";
import { BasketSidebarItem } from "../basketSidebarItem/ui/BasketSidebarItem";

export const BasketSidebar = observer(() => {
    const sidebarRef = useRef<HTMLDivElement>(null);
    const [isClosing, setIsClosing] = useState(false);
    const navigate = useNavigate();
    const { addNotification } = useNotifications();

    const handleClose = () => {
        setIsClosing(true);
        setTimeout(() => {
            basketStore.setVisibleBasket(false);
            setIsClosing(false);
        }, 300);
    };

    const HandleGoToBasket = () => {
        navigate('/basket');
        handleClose();
    };

    const HandleCreateOrder = async() => {
        const userId = authStore?.user?.id ?? '';
        const products = basketStore.items.map((product) => ({
            count: product.count,
            productId: product.productId
        } as OrderItem));
        
        const response = await OrderApi.CreateNewOrder(userId, products);
        
        if(response.success && response.status === 200){
            basketStore.clear();
            navigate('/Order');
            addNotification({
                header: 'Заказ сформирован',
                body: `Ваш заказ [${response.data}] был успешно сформирован. Скоро с вами свяжется оператор.`
            })
        }
        handleClose();
    }
    

    useEffect(() => {
        const handleClickOutside = (event: MouseEvent) => {
        if (
            sidebarRef.current &&
            !sidebarRef.current.contains(event.target as Node)
        ) {
            handleClose();
        }
        };

        if (basketStore.isVisibleBasket && !isClosing) {
        document.addEventListener("mousedown", handleClickOutside);
        }

        return () => {
        document.removeEventListener("mousedown", handleClickOutside);
        };
    }, [basketStore.isVisibleBasket, isClosing]);

    if (!basketStore.isVisibleBasket && !isClosing) return null;

    return (
        <>
            <div
                className={`${styles.overlay} ${isClosing ? styles["overlay-exit"] : ""}`}
                onClick={handleClose}
            />

            <div
                ref={sidebarRef}
                className={`${styles.container} ${isClosing ? styles["container-exit"] : ""}`}
            >
                <h2 className={styles.h2}>Ваша корзина</h2>
                <div className={styles.body}>
                    {(basketStore?.items?.length !== 0) ?
                    basketStore.items?.map((product) => (
                        <BasketSidebarItem key={product.productId} basketItem={product} />
                    ))
                    : (<div className={styles.noBasketProducts}>
                        Ваша корзина пуста.
                    </div>)}
                </div>
                {(basketStore.totalPrice !==0) && 
                <div className={styles.resultPriceContainer}>
                    <div className={styles.priceText}>
                        Итого: 
                    </div>
                    <div>
                        {basketStore.totalPrice} руб.
                    </div>
                </div>}
                <div className={styles.footer}>
                    <Button content="Оформить заказ" className={styles.createOrder} onClick={HandleCreateOrder} />
                    <Button content="Перейти в корзину" className={styles.toBasket} onClick={HandleGoToBasket}/>
                </div>
            </div>
        </>
    );
});