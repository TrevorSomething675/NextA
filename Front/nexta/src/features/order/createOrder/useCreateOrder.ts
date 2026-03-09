import { useNavigate } from "react-router-dom";
import { useNotifications } from "../../../shared/contexts/notifications/NotificationsContext";
import { OrderApi } from "../../../shared/http/order/orderApi";
import { OrderItem } from "../../../entities/order/models/orderItem";
import basketStore from "../../../shared/stores/basket/basketStore";
import orderStore from "../../../shared/stores/order/orderStore";

export const useCreateOrder = () => {
    const navigate = useNavigate();
    const { addNotification } = useNotifications();

    const createOrder = async(userId:string, products:OrderItem[]) => {
        const response = await OrderApi.CreateNewOrder(userId, products);
        
        if(response.success && response.status === 200){
            basketStore.clear();
            
            const newOrdersResponse = await OrderApi.Get(userId, '', 1, 8);
            
            if(newOrdersResponse.success && newOrdersResponse.status === 200){
                orderStore.setOrderItems(newOrdersResponse.data.items);
            }
            
            navigate('/Order');
            addNotification({
                header: 'Заказ сформирован',
                body: `Ваш заказ был успешно сформирован. Скоро с вами свяжется оператор.`
            })
        }
    }
    return { createOrder }
}