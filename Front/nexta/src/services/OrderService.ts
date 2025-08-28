import { CreateNewOrderRequest, CreateNewOrderResponse } from "../http/models/order/CreateNewOrderRequest";
import { GetLegacyOrdersForUserRequest, GetLegacyOrdersForUserResponse } from "../http/models/order/GetLegacyOrders";
import { GetOrdersForUserRequest, GetOrdersForUserResponse } from "../http/models/order/GetOrdersForUserFilter";
import api from "../http/api";
import axios from 'axios';
import { ErrorResponseModel } from "../shared/models/ErrorResponseModel";
import { ApiResponse } from "../http/BaseResponse";

class OrderService{
    static async GetOrdersForUser(request:GetOrdersForUserRequest) : Promise<ApiResponse<GetOrdersForUserResponse, ErrorResponseModel>>{
        try{
            const response = await api.get<GetOrdersForUserResponse>('Orders/GetOrdersForUser', {
                params: {
                    userId: request.userId,
                    pageSize: request.pageSize,
                    pageNumber: request.pageNumber
                }
            });
            return {
                success: true,
                data: response.data,
                status: response.status
            }
        } catch(error){
            if(axios.isAxiosError(error) && error.response){
                return {
                    success: false,
                    data: error.response.data as ErrorResponseModel,
                    status: error.response.status
                };
            } else {
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
    static async GetLegacyOrdersForUser(request:GetLegacyOrdersForUserRequest) : Promise<ApiResponse<GetLegacyOrdersForUserResponse, ErrorResponseModel>>{
        try{
            const response = await api.get<GetOrdersForUserResponse>('Orders/GetLegacyOrdersForUser', {
                params: {
                    userId: request.userId,
                    pageSize: request.pageSize,
                    pageNumber: request.pageNumber
                }
            });
            return {
                success: true,
                data: response.data,
                status: response.status
            }
        } catch(error){
            if(axios.isAxiosError(error) && error.response){
                return {
                    success: false,
                    data: error.response.data as ErrorResponseModel,
                    status: error.response.status
                };
            } else {
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }

    static async CreateNewOrder(request:CreateNewOrderRequest) : Promise<ApiResponse<CreateNewOrderResponse, ErrorResponseModel>>{
        try{
            const response = await api.post<CreateNewOrderResponse>('Order/CreateNewOrder', request);
            return {
                success: true,
                data: response.data,
                status: response.status
            }
        } catch(error){
            if(axios.isAxiosError(error) && error.response){
                return {
                    success: false,
                    data: error.response.data as ErrorResponseModel,
                    status: error.response.status
                };
            } else {
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }

    static async Delete(id:string){
        try{
            const response = await api.post<string>(`Order/Delete/${id}`);
            return {
                success: true,
                data: response.data,
                status: response.status
            }
        }
        catch(error){
            if(axios.isAxiosError(error) && error.response){
                return {
                    success: false,
                    data: error.response.data as ErrorResponseModel,
                    status: error.response.status
                };
            } else {
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
}

export default OrderService;