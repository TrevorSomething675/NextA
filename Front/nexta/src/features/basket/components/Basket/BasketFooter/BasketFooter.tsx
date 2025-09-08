import { observer } from "mobx-react";
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import OrderService from "../../../../../services/OrderService";
import basket from "../../../../../stores/basket";
import styles from './BasketFooter.module.css';
import { useNotifications } from "../../../../../shared/components/Notifications/Notifications";
import authStore from "../../../../../stores/AuthStore/authStore";
import Button from "../../../../../shared/components/Button/Button";
import orderStore from "../../../../../stores/orderStore";

const BasketFooter = observer(() => {
    const navigate = useNavigate();
    const {addNotification} = useNotifications();

    const [isLoading, setLoading] = useState(false);

    const handleCreateOrder = () =>{
        fetchData();
    }
    
    const fetchData = async() => {
        const userId = authStore?.user?.id ?? '';
        const productIds = basket.items.map((product) => product.productId);

        setLoading(true);

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
        setLoading(false);
    }

    return (basket?.items !== undefined) && (basket.items.length > 0) && <div className={styles.container}>
        <div className={styles.footerItem}>
            <Button content='Оформить заказ' className={styles.button} onClick={handleCreateOrder} isLoading={isLoading} />
        </div>
        <div className={styles.footerPrice}>
            <div className={styles.priceContainer}>Итого: {basket.totalPrice} руб.</div>
        </div>
    </div>
});

export default BasketFooter;