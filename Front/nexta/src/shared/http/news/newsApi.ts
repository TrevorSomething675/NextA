import api from "../api";
import { ApiResponse } from "../models/BaseResponse";
import { ErrorResponseModel } from "../../../sharedLegacy/models/ErrorResponseModel";
import { News } from "../../../entities/news/models/news";
import axios from 'axios';

export class NewsApi{
    static Get = async():Promise<ApiResponse<News[], ErrorResponseModel>> => {
        try{
            const response = await api.get<News[]>('News/Get');
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
    static Add = async(header:string, description:string, imageName:string, imageBase64String:string):Promise<ApiResponse<News, ErrorResponseModel>> => {
        try{
            const request = {
                header,
                description,
                imageName,
                imageBase64String
            }
            const response = await api.post<News>('Admin/News/Add', request);
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
    static Delete = async(id:string):Promise<ApiResponse<string, ErrorResponseModel>> => {
        try{
            const response = await api.delete<string>(`Admin/News/Delete/${id}`);
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