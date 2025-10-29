import api from "../api";
import { ApiResponse } from "../models/BaseResponse";
import { ErrorResponseModel } from "../../../sharedLegacy/models/ErrorResponseModel";
import { PagedData } from "../../../sharedLegacy/models/PagedDataT";
import { Order } from "../../../entities/order/models/order";
import axios from 'axios';
import { OrderItem } from "../../../entities/order/models/orderItem";
import qs from "qs";
import { Product } from "../../../entities/product/models/product";

export class OrderApi{
    static AddProduct = async(userId:string, productId:string, count:number):Promise<ApiResponse<Product, ErrorResponseModel>> => {
        try{
            const request = {
                userId,
                productId,
                count
            }
            const response = await api.post<Product>('Admin/Orders/AddProduct', request);
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
    static DeleteProduct = async(orderId:string, productId:string):Promise<ApiResponse<OrderItem, ErrorResponseModel>> => {
        try{
            const response = await api.delete<OrderItem>('Admin/Orders/DeleteProduct', {
                params: {
                    orderId,
                    productId
                }
            });
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
    static Update = async(orderId:string, userId:string, status:number[], products:OrderItem[]):Promise<ApiResponse<string, ErrorResponseModel>> => {
        try{
            const request = {
                orderId,
                userId,
                status,
                products
            }
            const response = await api.patch<string>('Admin/Order/Update', request);
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
    static GetByUserId = async(searchTerm:string, statuses?:number[], pageNumber?:number, pageSize?:number):Promise<ApiResponse<PagedData<Order>, ErrorResponseModel>> => {
        try{
            const response = await api.get<PagedData<Order>>('Admin/Orders/Get', {
                params: {
                    searchTerm,
                    statuses,
                    pageNumber,
                    pageSize
                },
                paramsSerializer: params => qs.stringify(params, { arrayFormat: "repeat" })
            });
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