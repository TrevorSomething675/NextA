import AdminOrders from '../../../features/admin/adminOrders/adminOrders';
import styles from './adminOrdersPage.module.css';

const AdminOrdersPage = () => {
    return <div className={styles.container}>
        <AdminOrders />
    </div>
}

export default AdminOrdersPage;