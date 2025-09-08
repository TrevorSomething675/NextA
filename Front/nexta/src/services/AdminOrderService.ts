import api from "../http/api";
import { UpdateAdminOrderResponse } from "../features/admin/models/UpdateOrder/UpdateAdminOrderResponse";
import { UpdateAdminOrderRequest } from "../features/admin/models/UpdateOrder/UpdateAdminOrderRequest";
import { GetAdminOrdersRequest, GetAdminOrdersResponse } from "../features/admin/models/AdminOrder/GetAdminOrders";
import { ApiResponse } from "../http/BaseResponse";
import { ErrorResponseModel } from "../shared/models/ErrorResponseModel";
import axios from "axios";
import qs from "qs";
import {DeleteAdminProductFromOrderResponse } from "../features/admin/models/DeleteAdminProductFromOrder/DeleteAdminProductFromOrder";

class AdminOrderService{
    static async DeleteProductFromOrder(orderId:string, productId:string) : Promise<ApiResponse<DeleteAdminProductFromOrderResponse, ErrorResponseModel>>{
        try{
            const response = await api.delete<DeleteAdminProductFromOrderResponse>('Admin/Orders/Delete', {
                params: {
                    orderId, productId
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
            }
            else{
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }

    static async GetOrders(request:GetAdminOrdersRequest) : Promise<ApiResponse<GetAdminOrdersResponse, ErrorResponseModel>>{
        try{
            const response = await api.get<GetAdminOrdersResponse>('Admin/Orders/Get', {
                params: {
                    statuses: request.statuses,
                    searchTerm: request.searchTerm,
                    pageNumber: request.pageNumber,
                    pageSize: request.pageSize
                },
                paramsSerializer: params => qs.stringify(params, { arrayFormat: "repeat" })
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
            }
            else{
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }

    static async UpdateOrder(request:UpdateAdminOrderRequest){
        try{
            const response = await api.patch<UpdateAdminOrderResponse>('Admin/Orders/Update', request);
            return response.data;
        } catch(error){
            throw new Error('Сетевая ошибка или ошибка конфигурации');
        }
    }
}

export default AdminOrderService;