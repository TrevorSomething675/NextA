import { CreateNewOrderRequest, CreateNewOrderResponse } from "../features/order/models/CreateNewOrderRequest";
import { GetLegacyOrdersForUserRequest } from "../features/order/models/GetLegacyOrders";
import { GetOrdersForUserRequest, GetOrdersForUserResponse } from "../features/order/models/GetOrdersForUserFilter";
import api from "../http/api";
import axios from 'axios';


class OrderService{
    static async GetOrdersForUser(request:GetOrdersForUserRequest){
        try{
            const response = await api.post<GetOrdersForUserResponse>('Order/GetOrdersForUser', request);
            return response.data;
        } catch(error){
            if(axios.isAxiosError(error) && error.response){
                return error.response.data as GetOrdersForUserResponse
            } else {
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
    static async GetLegacyOrdersForUser(request:GetLegacyOrdersForUserRequest){
        try{
            const response = await api.post<GetOrdersForUserResponse>('Order/GetLegacyOrdersForUser', request);
            return response.data;
        } catch(error){
            if(axios.isAxiosError(error) && error.response){
                return error.response.data as GetOrdersForUserResponse
            } else {
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }

    static async CreateNewOrder(request:CreateNewOrderRequest){
        try{
            const response = await api.post<CreateNewOrderResponse>('Order/CreateNewOrder', request);
            return response.data;
        } catch(error){
            if(axios.isAxiosError(error) && error.response){
                return error.response.data as CreateNewOrderResponse
            } else {
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
}

export default OrderService;