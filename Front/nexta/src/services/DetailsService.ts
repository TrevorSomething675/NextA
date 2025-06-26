import axios from 'axios';
import api from "../http/api";
import { GetDetailsRequest, GetDetailsResponse } from '../features/details/models/GetDetails';
import { GetDetailRequest, GetDetailResponse } from '../features/detail/models/GetDetail';

class DetailsService{
    static async GetDetails(request:GetDetailsRequest){
        try{
            const response = await api.post<GetDetailsResponse>('Detail/GetAll', request);
            return response.data
        } catch(error){
            if(axios.isAxiosError(error) && error.response){
                return error.response.data as GetDetailsResponse
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
            const response = await api.post<GetDetailResponse>('Detail/Get', request);
            return response.data;
        } catch(error){
            if(axios.isAxiosError(error) && error.response){
                return error.response.data as GetDetailResponse
            }
            else{
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
}

export default DetailsService;