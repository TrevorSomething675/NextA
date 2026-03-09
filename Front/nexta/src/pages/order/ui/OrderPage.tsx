import { useEffect } from 'react';
import { useGetOrders } from '../../../features/order/getOrders/useGetOrders';
import styles from './OrderPage.module.css';
import authStore from '../../../shared/stores/auth/authStore';
import { OrdersContainer } from '../../../widgets/ui/order/ordersContainer/OrdersContainer';
import { observer } from 'mobx-react';

export const OrderPage = observer(() => {
    const { getOrders} = useGetOrders();

    useEffect(() => {
        fetchData();
    }, []);
    
    const fetchData = async() => {
        const userId = authStore.user.id ?? '';
        await getOrders(userId, '', 1, 8);
    }

    return <div className={styles.container}>
        <h2 className={styles.h2}>Активные заказы</h2>
        <OrdersContainer />
    </div>
});