import { GetNewsResponse } from "../features/news/models/NewsResponse";
import api from "../http/api";

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
            const response = await api.post<GetNewsResponse>('News/GetAll');
            return response.data
        } catch(error){
            throw new Error('Сетевая ошибка или ошибка конфигурации');
        }
    }
}

export default NewsService;