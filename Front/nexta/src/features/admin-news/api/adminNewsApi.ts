import { ErrorResponseModel } from "../../../sharedLegacy/models/ErrorResponseModel";
import { ApiResponse } from "../../../shared/http/models/BaseResponse";
import { AdminNews } from "../models/adminNews";
import api from "../../../shared/http/api";
import axios from 'axios';

export class AdminNewsApi{
    static Get = async():Promise<ApiResponse<AdminNews[], ErrorResponseModel>> => {
        try{
            const response = await api.get<AdminNews[]>('Admin/News/Get');
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
    static Add = async(header:string, description:string, imageName:string, imageBase64String:string):Promise<ApiResponse<AdminNews, ErrorResponseModel>> => {
        try{
            const request = {
                header,
                description,
                imageName,
                imageBase64String
            };
            const response = await api.post('Admin/News/Add', request);
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
            const response = await api.delete(`Admin/News/Delete/${id}`);
            return {
                success:true,
                data:response.data,
                status:response.status
            };
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