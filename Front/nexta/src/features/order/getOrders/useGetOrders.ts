import { OrderApi } from "../../../shared/http/order/orderApi"
import orderStore from "../../../shared/stores/order/orderStore";

export const useGetOrders = () => {

    const getOrders = async(userId:string, searchTerm:string, pageNumber:number, pageSize:number) => {
        const response = await OrderApi.Get(userId, searchTerm, pageNumber, pageSize);
        
        if(response.success && response.status === 200){
            orderStore.setOrderItems(response.data.items);
            
            return {
                success: response.success,
                status: response.status
            }
        }   
    }
    return { getOrders }
}