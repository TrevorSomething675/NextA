import { useNotifications } from "../../../shared/contexts/notifications/NotificationsContext";
import { BasketApi } from "../../../shared/http/basket/basketApi";
import basketStore from "../../../shared/stores/basket/basketStore";

export const useUpdateProductInBasket = () => {
    const { addNotification } = useNotifications();

    const updateBasketProduct = async(userId:string, productId:string, count:number) => {

        const response = await BasketApi.Update(userId, productId, count);
                
        if(response.success == true && response.status === 200){
            basketStore.changeProductCount(response.data.productId, response.data.count);
            
            addNotification({
                header: 'Товар обновлён!',
                body: 'Изменения успешно внесены.'
            });

            return {
                success: response.success,
                status: response.status
            }
        }
        else {
            return { 
                success: false,
                status: response.status
            }
        }
    }

    return { updateBasketProduct }
}