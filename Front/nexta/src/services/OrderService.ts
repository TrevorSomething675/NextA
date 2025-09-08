import { CreateNewOrderRequest, CreateNewOrderResponse } from "../http/models/order/CreateNewOrderRequest";
import { GetLegacyOrdersForUserRequest, GetLegacyOrdersForUserResponse } from "../http/models/order/GetLegacyOrders";
import { GetOrdersForUserResponse } from "../http/models/order/GetOrdersForUser";
import api from "../http/api";
import axios from 'axios';
import { ErrorResponseModel } from "../shared/models/ErrorResponseModel";
import { ApiResponse } from "../http/BaseResponse";

class OrderService{
    static async GetOrdersForUser(userId:string, pageSize?:number, pageNumber?:number) : Promise<ApiResponse<GetOrdersForUserResponse, ErrorResponseModel>>{
        try{
            const response = await api.get<GetOrdersForUserResponse>('Orders/GetOrdersForUser', {
                params: {
                    userId: userId,
                    pageSize: pageSize,
                    pageNumber: pageNumber
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

    static async CreateNewOrder(userId:string, productIds:string[]) : Promise<ApiResponse<CreateNewOrderResponse, ErrorResponseModel>>{
        try{
            const request: CreateNewOrderRequest = {userId: userId, productIds: productIds};
            const response = await api.post<CreateNewOrderResponse>('Orders/CreateNewOrder', request);
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
            const response = await api.delete<string>(`Orders/Delete/${id}`);
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