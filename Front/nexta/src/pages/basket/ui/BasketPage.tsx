import { observer } from 'mobx-react';
import styles from './BasketPage.module.css';
import { useNavigate } from 'react-router-dom';
import { useNotifications } from '../../../shared/contexts/notifications/NotificationsContext';
import { useState } from 'react';
import authStore from '../../../shared/stores/auth/authStore';
import { OrderApi } from '../../../shared/http/order/orderApi';
import { OrderItem } from '../../../entities/order/models/orderItem';
import basketStore from '../../../shared/stores/basket/basketStore';
import { BasketContainer } from '../../../widgets/ui/basket/basketContainer/BasketContainer';

export const BasketPage = observer(() => {
    const navigate = useNavigate();
    const {addNotification} = useNotifications();

    /*
    const [isLoading, setLoading] = useState(false);

    const handleCreateOrder = () =>{
        fetchData();
    }
    
    const fetchData = async() => {
        const userId = authStore?.user?.id ?? '';
        const products = basketStore.items.map((product) => ({
            productId:product.productId,
            product:product.product
        }) as OrderItem);

        setLoading(true);

        const response = await OrderApi.CreateNewOrder(userId, products);
        
        if(response.success && response.status === 200){
            basketStore.clear();
            
            const newOrdersResponse = await OrderService.GetOrdersForUser(userId);

            if(newOrdersResponse.success && newOrdersResponse.status === 200){
                orderStore.setOrderItems(newOrdersResponse.data.data.items);
            }  
            navigate('/Order');
            addNotification({
                header: 'Заказ сформирован',
                body: `Ваш заказ [${response.data}] был успешно сформирован. Скоро с вами свяжется оператор.`
            })
        }
        setLoading(false);
    }
    */

    return <div className={styles.container}>
        <h2 className={styles.h2}>Корзина</h2>
        <BasketContainer />
    </div>
});