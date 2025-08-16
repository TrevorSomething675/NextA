import { Order, OrderStatus } from "../../../../shared/entities/Order";
import { useNavigate } from "react-router-dom";
import styles from './AdminOrderItem.module.css';
import { useState } from "react";
import AdminOrderService from "../../services/AdminOrderService";
import { UpdateAdminOrderRequest } from "../../models/UpdateOrder.ts/UpdateAdminOrderRequest";
import OrderService from "../../../../services/OrderService";
import Button from "../../../../shared/components/Button/Button";
import { useNotifications } from "../../../../shared/components/Notifications/Notifications";
import RightArrowSvg from "../../../../shared/svgs/RightArrowSvg/RightArrowSvg";
import { Detail } from "../../../../shared/entities/Detail";
import { AdminAddDetailToOrderRightBar } from "../AdminAddDetailToOrderRightBar/AdminAddDetailToOrderRightBar";

const AdminOrderItem: React.FC<{ order: Order}> = ({ order: initialOrder}) => {
    const [order, setOrder] = useState<Order>(initialOrder);
    const [originalOrder, setOriginalOrder] = useState<Order>(initialOrder);
    const [isDeleted, setIsDeleted] = useState(false);
    const { addNotification } = useNotifications();
    const navigate = useNavigate();
    const [isActiveRightBar, setActiveRightBar] = useState(false);

    const getColorForStatus = (status: any) => {
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

    const handleAddNewDetail = (detail: Detail, count: number) => {
    if (!detail || count < 1) return;
    
    setOrder(prevOrder => {
        const currentDetails = prevOrder.details || [];
        const currentOrderDetails = prevOrder.orderDetails || [];
        
        const existingDetailIndex = currentDetails.findIndex(d => d.id === detail.id);
        
        if (existingDetailIndex >= 0) {
            const updatedOrderDetails = currentOrderDetails.map(od => 
                od.detailId === detail.id 
                    ? { ...od, count: (od.count || 0) + count } 
                    : od
            );
            
            return {
                ...prevOrder,
                orderDetails: updatedOrderDetails,
                details: currentDetails.map(d => 
                    d.id === detail.id 
                        ? { ...d, count: (d.count || 0) + count } 
                        : d
                )
            };
        } else {
            const newOrderDetail = {
                detailId: detail.id,
                count: count,
                order: prevOrder,
                orderId: prevOrder.id,
                detail: { ...detail, count: count }
            };
            
            return {
                ...prevOrder,
                orderDetails: [...currentOrderDetails, newOrderDetail],
                details: [...currentDetails, { ...detail, count: count }]
            };
        }
    });
};

    const handleChangeSelectStatus = (status: number) => {
        setOrder(prevOrder => ({
            ...prevOrder,
            status: status
        }));
    }

    const handleCloseRightBar = () =>{
        setActiveRightBar(false);
    }

    const handleDeleteOrder = async () => {
        const response = await OrderService.Delete(order.id);
        if (response !== '') {
            setIsDeleted(true);
        }
    }

    const handleDeleteDetailFromOrder = async (detailId: string) => {
        setOrder(prevOrder => ({
            ...prevOrder,
            orderDetails: prevOrder?.orderDetails?.filter(d => d?.detailId !== detailId),
            details: prevOrder?.details?.filter(d => d.id !== detailId) ?? []
        }));
    }

    const handleChangeDetailCount = (detailId: string, newCount: number) => {
        if (newCount < 1) return;
        
        setOrder(prevOrder => ({
            ...prevOrder,
            orderDetails: prevOrder.orderDetails.map(od => 
                od.detailId === detailId
                    ? { ...od, count: newCount } 
                    : od
            )
        }));
    }  

    const goToDetailPage = (id: string) => {
        navigate(`/Admin/Detail/${id}`);
    }

    const handleCancelChanges = () => {
        setOrder({ ...originalOrder });
    }

    const handleSaveChanges = async () => {
        try {
            const request: UpdateAdminOrderRequest = {
                id: order.id,
                orderDetails: order.orderDetails.map(od => ({
                    detailId: od.detailId,
                    count: od.count,
                    order: order,
                    orderId: order.id,
                    detail: {} as Detail
                })),
                status: order.status
            };
            const response = await AdminOrderService.UpdateOrder(request);
            if (response) {
                setOriginalOrder({ ...order });
                addNotification({
                    header: 'Заказ успешно отредактирован'
                });
            }
        } catch (error) {
            console.error('Ошибка при сохранении изменений:', error);
        }
    }

    if (isDeleted) {
        return null;
    }

    return (
        <li className={styles.li}>
            {isActiveRightBar && <AdminAddDetailToOrderRightBar 
                orderId={order.id} 
                onClose={handleCloseRightBar} 
                onAddDetail={handleAddNewDetail}
            />}
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
                    <button className={styles.deleteOrderBtn} onClick={handleDeleteOrder} type="button">Удалить заказ</button>
                </div>
            </div>
            <div className={styles.orderSubHeader} style={{ backgroundColor: getColorForStatus(order.status) }}>
                <h2 className={styles.h2}>
                    Заказ №: [{order?.id}]
                </h2>
            </div>
            <table className={styles.table}>
                <thead className={styles.thead}>
                    <tr className={styles.tr}>
                        <th>Название</th>
                        <th>Артикул</th>
                        <th>Описание</th>
                        <th>Стоимость, ₽</th>
                        <th></th>
                        <th>Кол-во, шт</th>
                        <th></th>
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
                            <td>{orderDetail.detail.article}</td>
                            <td>{orderDetail.detail.description}</td>
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
                            <td>x</td>
                            <td>
                                <input
                                    type="number"
                                    min="1"
                                    value={orderDetail.count}
                                    onChange={(e) => handleChangeDetailCount(orderDetail.detailId, parseInt(e.target.value) || 1)}
                                    className={styles.countInput}
                                />
                            </td>
                            <td className={styles.trashContainer}>
                                <button className={styles.removeBasketBtn} onClick={async () => handleDeleteDetailFromOrder(orderDetail.detailId)}>
                                    <svg xmlns="http://www.w3.org/2000/svg" className={styles.trash} fill="currentColor" viewBox="0 0 16 16">
                                        <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0z" />
                                        <path d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4zM2.5 3h11V2h-11z" />
                                    </svg>
                                </button>
                            </td>
                        </tr>
                    )}
                    <tr className={styles.addDetailItem}>
                        <td>
                            <button 
                                className={styles.addDetailBtn}
                                    onClick={() => setActiveRightBar(true)}>
                                Добавить детель 
                                <RightArrowSvg />
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div className={styles.orderBody}></div>
            <div className={styles.orderFooter}>
                <div className={styles.orderFooter}>
                    <Button
                        onClick={handleSaveChanges}
                        content={'Сохранить изменения'}
                        className={styles.saveBtn}
                    />
                    <Button
                        onClick={handleCancelChanges}
                        content={'Отменить изменения'}
                        className={styles.rejectChangesbtn}
                    />
                </div>
                <div className={styles.orderStatus}>
                    <span className={styles.totalSumText}>
                        К оплате:
                    </span>
                    <span className={styles.totalSum}>
                        {(order.orderDetails.reduce((total, orderDetail) => total + (orderDetail.count * orderDetail.detail.newPrice), 0))} руб.
                    </span>
                    <span>Статус заказа: </span>
                    <select
                        value={order.status}
                        className={styles.select}
                        style={{ color: getColorForStatus(order.status) }}
                        onChange={((e) => handleChangeSelectStatus(Number(e.target.value)))}
                    >
                        <option value={0} style={{ color: getColorForStatus(0) }}>Принят</option>
                        <option value={1} style={{ color: getColorForStatus(1) }}>В работе</option>
                        <option value={2} style={{ color: getColorForStatus(2) }}>Отменён</option>
                        <option value={3} style={{ color: getColorForStatus(3) }}>Готов к выдаче</option>
                        <option value={4} style={{ color: getColorForStatus(4) }}>Завершён</option>
                    </select>
                </div>
            </div>
        </li>
    )
}

export default AdminOrderItem;