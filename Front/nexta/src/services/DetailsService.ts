import GetDetailsRequest from "../models/details/GetDetailsRequest";
import axios from 'axios';
import api from "../http";
import ApiResponse from "../models/ApiResponse";
import GetDetailsResponse from "../models/details/GetDetailsResponse";

class DetailsService{
    static async GetDetails(request:GetDetailsRequest){
        try{
            const response = await api.post<ApiResponse<GetDetailsResponse>>('Detail/GetAll', request);
            return response.data
        } catch(error){
            if(axios.isAxiosError(error) && error.response){
                return error.response.data as ApiResponse<GetDetailsResponse>
            }
            else{
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
}

export default DetailsService;