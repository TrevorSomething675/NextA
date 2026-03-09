import { useNotifications } from "../../../shared/contexts/notifications/NotificationsContext";
import { BasketApi } from "../../../shared/http/basket/basketApi";
import basketStore from "../../../shared/stores/basket/basketStore";

export const useRemoveProductFromBasket = () => {
    const { addNotification } = useNotifications();

    const removeProductFromBasket = async(productId:string, userId:string) => {

        const response = await BasketApi.DeleteProductFromBasket(userId, productId);

        if(response.success && response.status === 200)
        {
            addNotification({
                header: 'Товар успешно удалён'
            });
            basketStore.deleteBasketProduct(response.data);
            
            return {
                success: response.success,
                status: response.status
            }
        } else if (!response.success && response.status === 409){
            return {
                success: response.success,
                status: response.status
            }
        }
    }
    
    return { removeProductFromBasket }
}