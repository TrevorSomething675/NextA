import { Order, OrderStatus } from "../../../../shared/entities/Order";
import { useNavigate } from "react-router-dom";
import styles from './AdminOrderItem.module.css';
import { useState } from "react";

const AdminOrderItem:React.FC<{order:Order}> = ({order}) => {    
    const [isActiveBtn, setActiveBtn] = useState<boolean>(false);
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
        <div className={styles.orderHeader}>
            <div className={styles.userContainer}>
                <div className={styles.userItem}>
                    ФИО: {order?.user?.firstName} {order?.user?.lastName} {order?.user?.middleName}
                </div>
                    {(order?.user?.phone !== null) 
                    ?
                    <div className={styles.userItem}>
                        {order.user.phone}
                    </div>
                    :
                    <div className={styles.noPhone}>
                        У пользователя нет номера телефона
                    </div>
                    }
                <div className={styles.userItem}>
                    Почта: {order?.user?.email}
                </div>
            </div>
        </div>
        <div className={styles.orderSubHeader} style={{backgroundColor:getColorForStatus(order.status)}}>
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
                        
                    </th>
                    <th>
                        Кол-во, шт
                    </th>
                </tr>
            </thead>
            <tbody>
                {order.orderDetails.map((orderDetail) => 
                    <tr className={styles.tr} key={orderDetail.detailId}>
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
                                {orderDetail.detail.newPrice} руб.
                            </span>
                            {(orderDetail.detail.oldPrice !== undefined && orderDetail.detail.oldPrice != 0) &&
                                <span className={styles.oldPrice}>
                                    {orderDetail.detail.oldPrice} руб.
                                </span>
                            }
                        </td>
                        <td>
                            x
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
                <button className={styles.saveBtn}>
                    Сохранить изменения
                </button>
                <button className={`${styles.rejectChangesbtn} ${!isActiveBtn ? styles.unActive : ''}`} disabled={!isActiveBtn}>
                    Отменить изменения
                </button>
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