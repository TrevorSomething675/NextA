import { useState } from 'react';
import { AdminOrderSearch } from '../../components/AdminOrderSearch/AdminOrderSearch';
import styles from './AdminOrdersPage.module.css';
import { AdminOrderItem } from '../../components/AdminOrderItem/AdminOrderItem';
import { Order } from '../../../../entities/order/models/order';

const AdminOrdersPage = () => {
    const [orders, setOrdersResponse] = useState<Order[]>([]);

    const handleOrdersResponse = (orders:Order[]) => {
        setOrdersResponse(orders);
    }

    return (
        <div className={styles.container}>
            <AdminOrderSearch onResponseChange={handleOrdersResponse} />
            <div>
                {orders !== undefined && orders?.map((order) =>
                    <ul key={order.id}>
                        <AdminOrderItem
                            order={order}
                            key={order.id}
                        />
                    </ul>
                )}
            </div>
        </div>
    );
}

export default AdminOrdersPage;