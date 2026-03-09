import { observer } from 'mobx-react';
import styles from './OrdersContainer.module.css';
import orderStore from '../../../../shared/stores/order/orderStore';
import { OrderItem } from '../orderItem/OrderItem';

export const OrdersContainer = observer(() => {
    return <div>
        {(orderStore.items !== undefined && orderStore.items.length > 0) ? (
            <ul>
                {orderStore.items?.map((order) => <OrderItem key={order.id} order={order} /> )}
            </ul>
            ) : (
                <div className={styles.noOrders}>
                    У вас пока нет заказов.
                </div>
            )}
    </div>
});