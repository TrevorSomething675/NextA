import { GetNewsResponse } from "../features/news/models/NewsResponse";
import api from "../http/api";
import axios from 'axios';
import { ErrorResponseModel } from "../shared/models/ErrorResponseModel";
import { AddNewsForm } from "../features/admin/models/AddNews/AddNews";
import { DeleteNewsRequest } from "../features/admin/models/DeleteNews/DeleteNewsRequest";
import { DeleteNewsResponse } from "../features/admin/models/DeleteNews/DeleteNewsResponse";

class NewsService{
    static async GetNews(){
        try{
            const response = await api.post<GetNewsResponse>('News/GetAll');
            return response.data
        } catch(error){
            throw new Error('Сетевая ошибка или ошибка конфигурации');
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
    static async Delete(request:DeleteNewsRequest){
        try{
            const response = await api.post<DeleteNewsResponse>('Admin/News/Delete', request);
            return response.data;
        } catch(error){
            if(axios.isAxiosError(error) && error.response){
                throw error.response.data as ErrorResponseModel;
            } else {
                throw error;
            }
        }
    }
}

export default NewsService;