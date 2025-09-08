import { useNavigate } from "react-router-dom";
import styles from './OrderItem.module.css';
import { OrderStatus, UserOrder } from "../../../../models/order/UserOrder";

const OrderItem:React.FC<{order:UserOrder}> = ({order}) => {
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
            return '#1c6cb8';
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
        navigate(`/Product/${id}`);
    }

    return <li className={styles.li}>
        <div className={styles.orderHeader} style={{backgroundColor:getColorForStatus(order.status)}}>
            <h2 className={styles.h2}>
                Заказ №: [{order?.id}]
            </h2>
            <h2 className={styles.h2}>
                Дата оформления {order?.createdDate}
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
                        
                    </th>
                    <th>
                        Кол-во, шт
                    </th>
                </tr>
            </thead>
            <tbody>
                {order.orderProducts !== undefined && order?.orderProducts?.map((product) => 
                    <tr className={styles.tr} key={product.id}>
                        <td>
                            <button onClick={() => goToDetailPage(product.id)} className={styles.button}>
                                {product.name}
                            </button>
                        </td>
                        <td>
                            {product.article}
                        </td>
                        <td>
                            {product.description}
                        </td>
                        <td>
                            <span className={styles.newPrice}>
                                {product.newPrice * product.count} руб.
                            </span>
                            {(product.oldPrice !== undefined && product.oldPrice != 0) &&
                                <span className={styles.oldPrice}>
                                    {product.oldPrice * product.count} руб.
                                </span>
                            }
                        </td>
                        <td>
                            x
                        </td>
                        <td>
                            {product.count}
                        </td>
                    </tr>
                )}
            </tbody>
        </table>
        <div className={styles.orderBody}>
        </div>
        <div className={styles.orderFooter}>
            <div className={styles.orderStatus}>
                <span>Статус заказа: </span>
                <span style={{color:getColorForStatus(order.status)}}>{statusLabel[order.status]}</span>
            </div>
            <div>
                <span className={styles.orderQuestion}>Есть вопросы по заказу?</span>
                <span className={styles.number}>+7 (915) 562-95-13</span>
            </div>
            <div>
                <span className={styles.totalSumText}>
                    К оплате: 
                </span>
                <span className={styles.totalSum}>
                    {(order?.orderProducts?.reduce((total, product) => total + (product.count * product.newPrice), 0))} руб.
                </span>
            </div>
        </div>
    </li>
}

export default OrderItem;