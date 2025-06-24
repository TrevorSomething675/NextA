import api from "../http/api";
import NewsResponse from "../models/news/newsResponse";

class NewsService{
    static async GetNews(){
        try{
            const response = await api.post<NewsResponse>('News/GetAll');
            return response.data
        } catch(error){
            throw new Error('Сетевая ошибка или ошибка конфигурации');
        }
    }
}

export default NewsService;