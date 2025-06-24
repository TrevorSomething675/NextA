import GetDetailsRequest from "../models/details/GetDetailsRequest";
import axios from 'axios';
import api from "../http/api";
import GetWarehouseResponse from "../models/warehouse/GetWarehouseResponse";

class WarehouseService{
    static async GetDetails(request:GetDetailsRequest){
        try {
            const response = await api.post<GetWarehouseResponse>('Warehouse/Get', request);
            return response.data
        } catch(error) {
            if(axios.isAxiosError(error) && error.response){
                return error.response.data as GetWarehouseResponse
            }
            else{
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
}

export default WarehouseService;