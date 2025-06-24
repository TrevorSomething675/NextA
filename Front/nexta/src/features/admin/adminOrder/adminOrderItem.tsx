import styles from './adminOrderItem.module.css';
import Order, { OrderStatus } from "../../../models/Order";
import { useNavigate } from 'react-router-dom';

const AdminOrderItem:React.FC<{order:Order}> = ({order}) => {
    const navigate = useNavigate();
    const statusLabel = {
        [OrderStatus.Unknown]: 'Неизвестный статус',
        [OrderStatus.Accepted]: 'Принят',
        [OrderStatus.InProgress]: 'В работе',
        [OrderStatus.Canceled]: 'Отменён',
        [OrderStatus.Ready]: 'Готов к выдаче',
        [OrderStatus.Complete]: 'Завершён'
    }

    const getColorForStatus = (status:any) => {
        switch (status) {
            case OrderStatus.Accepted:
            return '#1e7309';
            case OrderStatus.InProgress:
            return '#ed7e00';
            case OrderStatus.Canceled:
            return '#850f16';
            case OrderStatus.Ready:
            return '#1e7309';
            case OrderStatus.Complete:
            return 'gray';
            case OrderStatus.Unknown:
            default:
            return 'gray';
        }
    }

    const goToDetailPage = (id:string) => {
        navigate(`/Detail/${id}`);
    }

    return <li className={styles.li}>
        <div className={styles.orderHeader} style={{backgroundColor:getColorForStatus(order.status)}}>
            <h2 className={styles.h2}>
                Заказ №: [{order?.id}]
            </h2>
        </div>
        <table className={styles.table}>
            <thead className={styles.thead}>
                <tr className={styles.tr}>
                    <th>
                        Название
                    </th>
                    <th>
                        Артикул
                    </th>
                    <th>
                        Описание
                    </th>
                    <th>
                        Стоимость, ₽
                    </th>
                    <th>
                        Кол-во, шт
                    </th>
                </tr>
            </thead>
            <tbody>
                {order.orderDetails.map((orderDetail) => 
                    <tr className={styles.tr} key={orderDetail.detail.id}>
                        <td>
                            <button onClick={() => goToDetailPage(orderDetail.detail.id)} className={styles.button}>
                                {orderDetail.detail.name}
                            </button>
                        </td>
                        <td>
                            {orderDetail.detail.article}
                        </td>
                        <td>
                            {orderDetail.detail.description}
                        </td>
                        <td>
                            <span className={styles.newPrice}>
                                {orderDetail.detail.newPrice * orderDetail.count} руб.
                            </span>
                            {(orderDetail.detail.oldPrice !== undefined && orderDetail.detail.oldPrice != 0) &&
                                <span className={styles.oldPrice}>
                                    {orderDetail.detail.oldPrice * orderDetail.count} руб.
                                </span>
                            }
                        </td>
                        <td>
                            {orderDetail.count}
                        </td>
                    </tr>
                )}
            </tbody>
        </table>
        <div className={styles.orderBody}>
        </div>
        <div className={styles.orderFooter}>
            <div>
                <span className={styles.orderQuestion}>Есть вопросы по заказу?</span>
                <span className={styles.number}>+7 915-562-95-13</span>
            </div>
            <div className={styles.orderStatus}>
                <span className={styles.totalSumText}>
                    К оплате: 
                </span>
                <span className={styles.totalSum}>
                    {(order.orderDetails.reduce((total, orderDetail) => total + (orderDetail.count * orderDetail.detail.newPrice), 0))} руб.
                </span>
                <span>Статус: </span>
                <span style={{color:getColorForStatus(order.status)}}>{statusLabel[order.status]}</span>
            </div>
        </div>
    </li>
}

export default AdminOrderItem;