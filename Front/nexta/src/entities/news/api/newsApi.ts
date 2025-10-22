import api from "../../../shared/http/api";
import { ApiResponse } from "../../../shared/http/models/BaseResponse";
import { ErrorResponseModel } from "../../../sharedLegacy/models/ErrorResponseModel";
import { News } from "../models/news";
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
}