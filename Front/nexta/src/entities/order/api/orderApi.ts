import api from "../../../shared/http/api";
import { ApiResponse } from "../../../shared/http/models/BaseResponse";
import { ErrorResponseModel } from "../../../sharedLegacy/models/ErrorResponseModel";
import { PagedData } from "../../../sharedLegacy/models/PagedDataT";
import { Order } from "../models/order";
import axios from 'axios';
import { OrderItem } from "../models/orderItem";

export class OrderApi{
    static Get = async(userId:string, searchTerm:string, pageNumber:number, pageSize:number):Promise<ApiResponse<PagedData<Order>, ErrorResponseModel>> => {
        try{
            const response = await api.get<PagedData<Order>>('Orders/Get', {
                params:{
                    userId,
                    searchTerm,
                    pageNumber,
                    pageSize
                }
            })
            return {
                success:true,
                data:response.data,
                status:response.status
            };
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
    static CreateNewOrder = async(userId:string, products:OrderItem[]):Promise<ApiResponse<string, ErrorResponseModel>> => {
        try{
            const request = {
                userId,
                products
            };
            const response = await api.post<string>('Orders/CreateNewOrder', request);
            return {
                success:true,
                data:response.data,
                status:response.status
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
    static UpdateStatus = async(orderId:string, status:number):Promise<ApiResponse<OrderItem, ErrorResponseModel>> => {
        try{
            const request = {
                orderId,
                status
            };
            const response = await api.patch('Orders/UpdateStatus', request);
            return {
                success:true,
                data:response.data,
                status:response.status
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
    static UpdateOrderProduct = async(orderId:string, productId:string, count:number):Promise<ApiResponse<OrderItem, ErrorResponseModel>> => {
        try{
            const request = {
                orderId,
                productId,
                count
            };
            const response = await api.patch('Orders/UpdateOrderProduct', request);
            return {
                success:true,
                data:response.data,
                status:response.status
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
    static Delete = async(orderId:string):Promise<ApiResponse<string, ErrorResponseModel>> => {
        try{
            const response = await api.delete(`Orders/Delete/${orderId}`);
            return {
                success:true,
                data:response.data,
                status:response.status
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