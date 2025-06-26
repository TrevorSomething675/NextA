import { observer } from "mobx-react";
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import OrderService from "../../../../services/OrderService";
import basket from "../../../../stores/basket";
import styles from './BasketFooter.module.css';
import { useNotifications } from "../../../../shared/components/Notifications/Notifications";
import { CreateNewOrderRequest } from "../../../order/models/CreateNewOrderRequest";
import authStore from "../../../../stores/AuthStore/authStore";

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
            navigate('/Order');
            addNotification({
                header: 'Заказ сформирован',
                body: `Ваш заказ [${response.order.id}] был успешно сформирован. Скоро с вами свяжется оператор.`
            })
        }
        setLoading(false);
    }   

    return (basket?.details !== undefined) && (basket.details.length > 0) && <div className={styles.container}>
        <div className={styles.footerItem}>
            <button className={styles.button} onClick={handleCreateOrder}>
                {isLoading ? 
                    (<img src="/loading2.gif" className={styles.loading}/>)
                    : 
                    (<p className={styles.p}>
                        Оформить заказ
                    </p>)
                }
            </button>
        </div>
        <div className={styles.footerPrice}>
            <div className={styles.priceContainer}>Итого: {basket.totalPrice} руб.</div>
        </div>
    </div>
});

export default BasketFooter;