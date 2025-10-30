import { observer } from "mobx-react";
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import styles from './BasketFooter.module.css';
import { useNotifications } from "../../../../../shared/contexts/notifications/NotificationsContext";
import orderStore from "../../../../../shared/stores/order/orderStore";
import authStore from "../../../../../shared/stores/auth/authStore";
import basketStore from "../../../../../shared/stores/basket/basketStore";
import { OrderApi } from "../../../../../shared/http/order/orderApi";
import { OrderItem } from "../../../../../entities/order/models/orderItem";
import { Button } from "../../../../../shared/ui";

const BasketFooter = observer(() => {
    const navigate = useNavigate();
    const {addNotification} = useNotifications();

    const [isLoading, setLoading] = useState(false);

    const handleCreateOrder = () =>{
        fetchData();
    }
    
    const fetchData = async() => {
        const userId = authStore?.user?.id ?? '';
        const products = basketStore.items.map((product) => ({
            count: product.count,
            productId: product.productId
        } as OrderItem));

        setLoading(true);

        const response = await OrderApi.CreateNewOrder(userId, products);
        
        if(response.success && response.status === 200){
            basketStore.clear();

            const newOrdersResponse = await OrderApi.GetByUserId(userId);

            if(newOrdersResponse.success && newOrdersResponse.status === 200){
                orderStore.setOrderItems(newOrdersResponse.data.items);
            }  
            
            navigate('/Order');
            addNotification({
                header: 'Заказ сформирован',
                body: `Ваш заказ был успешно сформирован. Скоро с вами свяжется оператор.`
            })
        }
        setLoading(false);
    }

    return (basketStore?.items !== undefined) && (basketStore.items.length > 0) && <div className={styles.container}>
        <div className={styles.footerItem}>
            <Button className={styles.button} onClick={handleCreateOrder} >
                Оформить заказ
            </Button>
        </div>
        <div className={styles.footerPrice}>
            <div className={styles.priceContainer}>Итого: {basketStore.totalPrice} руб.</div>
        </div>
    </div>
});

export default BasketFooter;