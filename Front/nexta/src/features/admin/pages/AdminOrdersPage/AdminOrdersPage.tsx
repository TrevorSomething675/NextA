import { useState } from 'react';
import { AdminOrderSearch } from '../../components/AdminOrderSearch/AdminOrderSearch';
import styles from './AdminOrdersPage.module.css';
import { AdminOrderItem } from '../../components/AdminOrderItem/AdminOrderItem';
import { UserOrder } from '../../../../models/order/UserOrder';

const AdminOrdersPage = () => {
    const [orders, setOrdersResponse] = useState<UserOrder[]>([]);

    const handleOrdersResponse = (orders:UserOrder[]) => {
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