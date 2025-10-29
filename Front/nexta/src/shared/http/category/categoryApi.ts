import api from "../api";
import { ApiResponse } from "../models/BaseResponse";
import { ErrorResponseModel } from "../../../sharedLegacy/models/ErrorResponseModel";
import { Category } from "../../../entities/category/models/category";
import axios from 'axios';

export class CategoryApi{
    static Get = async():Promise<ApiResponse<Category[], ErrorResponseModel>> => {
        try{
            const response = await api.get<Category[]>('Categories/Get');
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
    static Add = async(name:string):Promise<ApiResponse<string, ErrorResponseModel>> => {
        try{
            const request = {
                name
            };
            const response = await api.post<string>('Admin/Categories/Add', request);
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
    static Delete = async(name:string):Promise<ApiResponse<string, ErrorResponseModel>> => {
        try{
            const response = await api.delete(`Admin/Categories/Delete`, {
                params: {
                    name
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