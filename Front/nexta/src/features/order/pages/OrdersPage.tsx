import Orders from "../components/orders/Orders";
import styles from './OrdersPage.module.css';

const OrderPage = () => {
    return <div className={styles.container}>
        <h2 className={styles.h2}>Активные заказы</h2>
        <Orders />
    </div>
}

export default OrderPage;