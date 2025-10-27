import axios from 'axios';
import { ErrorResponseModel } from '../../../sharedLegacy/models/ErrorResponseModel';
import api from '../../../shared/http/api';
import { ApiResponse } from '../../../shared/http/models/BaseResponse';
import { PagedData } from '../../../sharedLegacy/models/PagedDataT';
import { User } from '../models/user';

export class UserApi{
    static Get = async(searchTerm:string, pageNumber:number, pageSize:number):Promise<ApiResponse<PagedData<User>, ErrorResponseModel>> => {
        try{
            const response = await api.get<PagedData<User>>('Admin/Users/Get', {
                params: {
                    searchTerm,
                    pageNumber,
                    pageSize
                }
            })
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
    static Delete = async(id:string):Promise<ApiResponse<string, ErrorResponseModel>> =>{
        try{
            const response = await api.delete(`Admin/Users/Delete/${id}`);
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