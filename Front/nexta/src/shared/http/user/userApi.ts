import axios from 'axios';
import { ErrorResponseModel } from '../../models/ErrorResponseModel';
import api from '../api';
import { ApiResponse } from '../models/BaseResponse';
import { PagedData } from '../../models/PagedDataT';
import { User } from '../../../entities/user/models/user';

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