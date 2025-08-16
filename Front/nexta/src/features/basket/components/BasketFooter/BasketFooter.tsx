import { observer } from "mobx-react";
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import OrderService from "../../../../services/OrderService";
import basket from "../../../../stores/basket";
import styles from './BasketFooter.module.css';
import { useNotifications } from "../../../../shared/components/Notifications/Notifications";
import { CreateNewOrderRequest } from "../../../order/models/CreateNewOrderRequest";
import authStore from "../../../../stores/AuthStore/authStore";
import Button from "../../../../shared/components/Button/Button";
import { GetOrdersForUserFilter } from "../../../order/models/GetOrdersForUserFilter";
import orderStore from "../../../../stores/orderStore";

const BasketFooter = observer(() => {
    const navigate = useNavigate();
    const {addNotification} = useNotifications();

    const [isLoading, setLoading] = useState(false);

    const handleCreateOrder = () =>{
        fetchData();
    }
    const fetchData = async() => {
        const request:CreateNewOrderRequest = {
            userId: authStore?.user?.id ?? '',
            detailIds: basket.details.map((detail) => detail.id)
        }
        setLoading(true);
        const response = await OrderService.CreateNewOrder(request);
        if(response){
            basket.clear();
            const userId = localStorage.getItem('userId') ?? '';     
            const filter: GetOrdersForUserFilter = {
                userId: userId,
            }
            const request = {
                filter: filter
            }
            const ordersResponse = await OrderService.GetOrdersForUser(request);
            orderStore.setTotalOrders(ordersResponse?.totalCount);

            navigate('/Order');
            addNotification({
                header: 'Заказ сформирован',
                body: `Ваш заказ [${response.id}] был успешно сформирован. Скоро с вами свяжется оператор.`
            })
        }
        setLoading(false);
    }   

    return (basket?.details !== undefined) && (basket.details.length > 0) && <div className={styles.container}>
        <div className={styles.footerItem}>
            <Button content='Оформить заказ' className={styles.button} onClick={handleCreateOrder} isLoading={isLoading} />
        </div>
        <div className={styles.footerPrice}>
            <div className={styles.priceContainer}>Итого: {basket.totalPrice} руб.</div>
        </div>
    </div>
});

export default BasketFooter;