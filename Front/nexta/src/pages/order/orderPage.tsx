import Orders from '../../components/order/orders';
import styles from './orderPage.module.css';

const OrderPage = () => {
    return <div className={styles.container}>
        <h2 className={styles.h2}>Заказы</h2>
        <div className={styles.ordersContainer}>
           <Orders />
        </div>
    </div>
}

export default OrderPage;