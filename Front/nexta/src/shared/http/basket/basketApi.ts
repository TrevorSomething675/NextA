import api from "../api";
import { ApiResponse } from "../models/BaseResponse";
import { ErrorResponseModel } from "../../models/ErrorResponseModel";
import { Basket } from "../../../entities/basket/models/basket";
import axios from 'axios';
import { BasketItem } from "../../../entities/basket/models/basketItem";
import { AddBasketProductRequest } from "../../../features/basket/models/addBasketProductRequest";

export class BasketApi{
    static GetByUserId = async(userId:string):Promise<ApiResponse<Basket, ErrorResponseModel>> => {
        try{
            const response = await api.get<Basket>(`Basket/Get/${userId}`);
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
    static AddProductToBasket = async(userId:string, productId:string, count:number):Promise<ApiResponse<BasketItem, ErrorResponseModel>> => {
        try{
            const request:AddBasketProductRequest = {
                userId,
                productId,
                count
            }
            const response = await api.post<BasketItem>('Basket/Add', request);
            return {
                success:true,
                data:response.data,
                status:response.status
            }
        }
        catch(error){
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
    static Update = async(userId:string, productId:string, count:number):Promise<ApiResponse<BasketItem, ErrorResponseModel>> => {
        try{
            const request = {
                userId,
                productId,
                count
            }
            console.warn(request);
            const response = await api.patch('Basket/Update', request);
            return {
                success:true,
                data:response.data,
                status: response.status
            }
        }
        catch(error){
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
    static DeleteProductFromBasket = async(userId:string, productId:string):Promise<ApiResponse<string, ErrorResponseModel>> => {
        try{
            const response = await api.delete('Basket/Delete', {
                params: {
                    userId:userId,
                    productId:productId
                }
            });
            return {
                success:true,
                data:response.data,
                status:response.status
            }
        }
        catch(error){
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
}