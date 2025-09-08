import { observer } from "mobx-react-lite";
import { useRef, useEffect, useState } from "react";
import styles from "./BasketSidebar.module.css";
import basket from "../../../../stores/basket";
import Button from "../../../../shared/components/Button/Button";
import { BasketSidebarItem } from "./BasketSidebarItem/BasketSidebarItem";
import { useNavigate } from "react-router-dom";
import authStore from "../../../../stores/AuthStore/authStore";
import OrderService from "../../../../services/OrderService";
import orderStore from "../../../../stores/orderStore";
import { useNotifications } from "../../../../shared/components/Notifications/Notifications";

export const BasketSidebar = observer(() => {
    const sidebarRef = useRef<HTMLDivElement>(null);
    const [isClosing, setIsClosing] = useState(false);
    const navigate = useNavigate();
    const {addNotification} = useNotifications();


    const handleClose = () => {
        setIsClosing(true);
        setTimeout(() => {
        basket.setVisibleBasket(false);
        setIsClosing(false);
        }, 300);
    };

    const HandleGoToBasket = () => {
        navigate('/basket');
        handleClose();
    };

    const HandleCreateOrder = async() => {
        const userId = authStore?.user?.id ?? '';
        const productIds = basket.items.map((product) => product.productId);

        const response = await OrderService.CreateNewOrder(userId, productIds);
        
        if(response.success && response.status === 200){
            basket.clear();

            const newOrdersResponse = await OrderService.GetOrdersForUser(userId);

            if(newOrdersResponse.success && newOrdersResponse.status === 200){
                orderStore.setOrderItems(newOrdersResponse.data.data.items);
            }  
            
            navigate('/Order');
            addNotification({
                header: 'Заказ сформирован',
                body: `Ваш заказ [${response.data.id}] был успешно сформирован. Скоро с вами свяжется оператор.`
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

        if (basket.isVisibleBasket && !isClosing) {
        document.addEventListener("mousedown", handleClickOutside);
        }

        return () => {
        document.removeEventListener("mousedown", handleClickOutside);
        };
    }, [basket.isVisibleBasket, isClosing]);

    if (!basket.isVisibleBasket && !isClosing) return null;

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
                <h2 className={styles.h2}>Ваши заказы</h2>
                <div className={styles.body}>
                    {basket.items?.map((product) => (
                        <BasketSidebarItem key={product.productId} product={product} />
                    ))}
                </div>
                <div className={styles.resultPriceContainer}>
                    <div className={styles.priceText}>
                        Итого: 
                    </div>
                    <div>
                        {basket.totalPrice} руб.
                    </div>
                </div>
                <div className={styles.footer}>
                    <Button content="Оформить заказ" className={styles.createOrder} onClick={HandleCreateOrder} />
                    <Button content="Перейти в корзину" className={styles.toBasket} onClick={HandleGoToBasket}/>
                </div>
            </div>
        </>
    );
});