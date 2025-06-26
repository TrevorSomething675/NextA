import axios from 'axios';
import api from "../http/api";
import { GetWarehouseRequest, GetWarehouseResponse } from '../features/details/models/GetWarehouse';

class WarehouseService{
    static async GetDetails(request:GetWarehouseRequest){
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