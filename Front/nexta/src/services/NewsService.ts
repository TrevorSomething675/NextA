import { GetNewsResponse } from "../features/news/models/NewsResponse";
import api from "../http/api";
import axios from 'axios';
import { ErrorResponseModel } from "../shared/models/ErrorResponseModel";
import { AddNewsForm } from "../features/admin/models/AddNews/AddNews";
import { DeleteNewsResponse } from "../features/admin/models/DeleteNews/DeleteNewsResponse";
import { ApiResponse } from "../http/BaseResponse";

class NewsService{
    static async Get() : Promise<ApiResponse<GetNewsResponse, ErrorResponseModel>>{
        try{
            const response = await api.get<GetNewsResponse>('News/Get');
            return {
                success: true,
                data: response.data,
                status: response.status
            }
        } catch(error){
            if(axios.isAxiosError(error) && error.response){
                return {
                    success: false,
                    data: error.response.data as ErrorResponseModel,
                    status: error.response.status
                };
            }
            else{
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
    static async Update(){
        try{
            const response = await api.post<GetNewsResponse>('News/GetAll');
            return response.data
        } catch(error){
            throw new Error('Сетевая ошибка или ошибка конфигурации');
        }
    }
    static async Add(){
        try{
            const response = await api.post<AddNewsForm>('Admin/News/Add');
            return response.data
        } catch(error){
            throw new Error('Сетевая ошибка или ошибка конфигурации');
        }
    }
    static async Delete(id:string) : Promise<ApiResponse<DeleteNewsResponse, ErrorResponseModel>>{
        try{
            const response = await api.delete<DeleteNewsResponse>(`Admin/News/Delete/${id}`);
            return {
                success: true,
                data: response.data,
                status: response.status
            }
        } catch(error){
            if(axios.isAxiosError(error) && error.response){
                return {
                    success: false,
                    data: error.response.data as ErrorResponseModel,
                    status: error.response.status
                };
            }
            else{
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
}

export default NewsService;