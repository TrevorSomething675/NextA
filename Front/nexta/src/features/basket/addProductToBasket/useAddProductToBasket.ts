import { useNotifications } from "../../../shared/contexts/notifications/NotificationsContext";
import { BasketApi } from "../../../shared/http/basket/basketApi";
import basketStore from "../../../shared/stores/basket/basketStore";

export const useAddProductToBasket = () => {
    const { addNotification } = useNotifications();

    const addProductToBasket = async(userId:string, productId:string, count:number) => {
        const response = await BasketApi.AddProductToBasket(userId, productId, count);
    
        if(response.success && response.status === 200)
        {
            addNotification({
                header: 'Товар добавлен в корзину'
            });
            basketStore.addBasketProduct(response.data);
                            
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

    return { addProductToBasket }
}