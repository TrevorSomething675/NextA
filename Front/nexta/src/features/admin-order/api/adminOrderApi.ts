import { ApiResponse } from "../../../shared/http/models/BaseResponse";
import api from "../../../shared/http/api";
import axios from 'axios';
import { ErrorResponseModel } from "../../../shared/models/ErrorResponseModel";
import { Order } from "../../../entities/order/models/order";
import qs from "qs";
import { Product } from "../../../models/Product";
import { OrderItem } from "../../../entities/order/models/orderItem";

export class AdminOrderApi{
    static Get = async(statuses:number[], searchTerm:string, pageNumber:number, pageSize:number):Promise<ApiResponse<Order[], ErrorResponseModel>> => {
        try{
            const response = await api.get<Order[]>('Admin/News/Get', {
                params:{
                    searchTerm,
                    pageNumber,
                    pageSize,
                    statuses
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
                    success:false,
                    data:error.response.data as ErrorResponseModel,
                    status: error.response.status
                }
            }
            else{
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
    static Add = async(orderId:string, productId:string, count:number):Promise<ApiResponse<Product, ErrorResponseModel>> => {
        try{
            const response = await api.post('Admin/News/Add', {
                productId,
                orderId,
                count
            });
            return{
                success:true,
                data:response.data,
                status:response.status
            }
        }
        catch(error){
            if(axios.isAxiosError(error) && error.response){
                return {
                    success:false,
                    data:error.response.data as ErrorResponseModel,
                    status: error.response.status
                }
            }
            else{
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
    static Update = async(id:string, userId:string, statuses:number[]):Promise<ApiResponse<Order, ErrorResponseModel>> => {
        try{
            const request = {
                id,
                userId,
                statuses
            };
            const response = await api.patch('Admin/Orders/Update', request);
            return {
                success:true,
                data:response.data,
                status:response.status
            }
        }
        catch(error){
            if(axios.isAxiosError(error) && error.response){
                return {
                    success:false,
                    data:error.response.data as ErrorResponseModel,
                    status: error.response.status
                }
            }
            else{
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
    static Delete = async(orderId:string, productId:string):Promise<ApiResponse<OrderItem, ErrorResponseModel>> => {
        try{
            const response = await api.delete('Admin/Orders/Delete', {
                params:{
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
                    success:false,
                    data:error.response.data as ErrorResponseModel,
                    status: error.response.status
                }
            }
            else{
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
}