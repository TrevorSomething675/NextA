import GetOrdersForUserResponse from "../models/order/GetOrdersForUserResponse";
import GetOrdersForUserRequest from "../models/order/GetOrdersForUserRequest";
import api from "../http";
import axios from 'axios';
import CreateNewOrderRequest from "../models/order/createNewOrder/CreateNewOrderRequest";
import CreateNewOrderResponse from "../models/order/createNewOrder/CreateNewOrderResponse";
import GetLegacyOrdersForUserRequest from "../models/order/getLegacyOrders/GetLegacyOrdersForUserRequest";

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