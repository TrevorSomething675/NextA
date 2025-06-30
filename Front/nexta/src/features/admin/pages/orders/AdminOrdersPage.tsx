import AdminOrders from '../../components/AdminOrders/AdminOrders';
import styles from './AdminOrdersPage.module.css';

const AdminOrdersPage = () => {
    return <div className={styles.container}>
        <AdminOrders />
    </div>
}

export default AdminOrdersPage;