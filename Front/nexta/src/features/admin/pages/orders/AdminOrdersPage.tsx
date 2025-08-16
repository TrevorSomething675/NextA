import { useState } from 'react';
import AdminOrders from '../../components/AdminOrders/AdminOrders';
import { AdminOrderSearch } from '../../components/AdminOrderSearch/AdminOrderSearch';
import styles from './AdminOrdersPage.module.css';
import GetAllOrdersResponse from '../../../../models/admin/GetAllOrders/GetAllOrdersResponse';

const AdminOrdersPage = () => {
    const [ordersResponse, setOrdersResponse] = useState({} as GetAllOrdersResponse);

    const handleOrdersResponse = (ordersResponse:GetAllOrdersResponse) => {
        setOrdersResponse(ordersResponse);
    }

    return (
        <div className={styles.container}>
            <AdminOrderSearch onResponseChange={handleOrdersResponse} />
            <AdminOrders response={ordersResponse} />
        </div>
    );
}

export default AdminOrdersPage;