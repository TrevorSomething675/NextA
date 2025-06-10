import { observer } from 'mobx-react';
import basket from '../../../stores/basket';
import styles from './basketFooter.module.css';
import OrderService from '../../../services/OrderService';
import CreateNewOrderRequest from '../../../models/order/createNewOrder/CreateNewOrderRequest';
import auth from '../../../stores/auth';
import { useNavigate } from 'react-router-dom';
import { useState } from 'react';
import { useNotifications } from '../../notifications/notifications';

const BasketFooter = observer(() => {
    const navigate = useNavigate();
    const {addNotification} = useNotifications();

    const [error, setError] = useState({} as string[]);

    const handleCreateOrder = () =>{
        fetchData();
    }
    const fetchData = async() => {
        const request:CreateNewOrderRequest = {
            userId: auth?.user?.id,
            detailIds: basket.details.map((detail) => detail.id)
        }
        const response = await OrderService.CreateNewOrder(request);
        if(response){
            basket.clear();
            navigate('/Order');
            addNotification({
                header: 'Заказ сформирован',
                body: `Ваш заказ [${response.order.id}] был успешно сформирован. Скоро с вами свяжется оператор.`
            })
        }/* ПЕРЕДЕЛАТЬ НА ПРОМИСЫ
        else {
            setError(response);
        } */
    }   

    return (basket?.details !== undefined) && (basket.details.length > 0) && <div className={styles.container}>
        <div className={styles.footerItem}>
            <button className={styles.button} onClick={handleCreateOrder}>Оформить заказ</button>
        </div>
        <div className={styles.footerPrice}>
            <div className={styles.priceContainer}>Итого: {basket.totalPrice} руб.</div>
        </div>
    </div>
});

export default BasketFooter;