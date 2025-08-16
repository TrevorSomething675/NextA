import { GetBasketDetailsRequest, GetBasketDetailsResponse } from "../models/GetBasketDetails";
import { AddBasketDetailRequest } from "../models/AddBasketDetail";
import { DeleteBasketDetail } from "../models/DeleteBasketDetail";
import api from "../../../http/api";
import axios from 'axios';
import { Detail } from "../../../shared/entities/Detail";
import { UpdateBasketDetailRequest } from "../models/UpdateBasketDetail";
import { OrderDetail } from "../../../shared/entities/OrderDetail";
import { ApiResponse } from "../../../http/BaseResponse";
import { ErrorResponseModel } from "../../../shared/models/ErrorResponseModel";

class BasketService{
    static async GetBasketDetails(data:GetBasketDetailsRequest) {
        try {
            const response = await api.post<GetBasketDetailsResponse>('Basket/Get', data)
            return response.data;
        } catch(error) {
            if(axios.isAxiosError(error) && error.response){
                return error.response.data as GetBasketDetailsResponse
            }
            else {
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }

    static async AddBasketDetail(data: AddBasketDetailRequest): Promise<ApiResponse<Detail, ErrorResponseModel>> {
        try {
            const response = await api.post<Detail>('Basket/Add', data);
            return { 
                success: true, 
                data: response.data, 
                status: response.status 
            };
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                return { 
                    success: false, 
                    data: error.response.data as ErrorResponseModel,
                    status: error.response.status 
                };
            }
            throw new Error('Сетевая ошибка или ошибка конфигурации');
        }
    }

    static async DeleteBasketDetail(data:DeleteBasketDetail) {
        try {
            const response = await api.post<Detail>('Basket/Delete', data)
            return response.data;
        } catch(error) {
            if(axios.isAxiosError(error) && error.response){
                return error.response.data as Detail
            } 
            else{
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
    static async UpdateBasketDetail(data:UpdateBasketDetailRequest) {
        try {
            const response = await api.post<OrderDetail>('Basket/Update', data)
            return { success: true, data: response.data, status: response.status };
        } catch(error) {
            if (axios.isAxiosError(error) && error.response) {
                return { 
                    success: false, 
                    data: error.response.data, 
                    status: error.response.status 
                };
            }
            else{
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
}

export default BasketService;