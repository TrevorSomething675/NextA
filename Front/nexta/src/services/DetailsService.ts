import GetDetailsResponse from "../models/details/GetDetailsResponse";
import GetDetailsRequest from "../models/details/GetDetailsRequest";
import ApiResponse from "../models/ApiResponse";
import axios from 'axios';
import api from "../http";
import GetDetailRequest from "../models/detail/GetDetailRequest";
import GetDetailResponse from "../models/detail/GetDetailResponse";

class DetailsService{
    static async GetDetails(request:GetDetailsRequest){
        try{
            const response = await api.post<ApiResponse<GetDetailsResponse>>('Detail/GetAll', request);
            return response.data
        } catch(error){
            if(axios.isAxiosError(error) && error.response){
                return error.response.data as ApiResponse<GetDetailsResponse>
            } else {
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
    static async GetDetail(id:string){
        try{
            const request:GetDetailRequest = {
                id: id
            } 
            const response = await api.post<ApiResponse<GetDetailResponse>>('Detail/Get', request);
            return response.data;
        } catch(error){
            if(axios.isAxiosError(error) && error.response){
                return error.response.data as ApiResponse<GetDetailResponse>
            }
            else{
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
}

export default DetailsService;