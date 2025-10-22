import api from "../../../shared/http/api";
import { ApiResponse } from "../../../shared/http/models/BaseResponse";
import { ErrorResponseModel } from "../../../sharedLegacy/models/ErrorResponseModel";
import { AdminNews } from "../models/adminNews";
import axios from 'axios';

export class AdminNewsApi{
    static Get = async():Promise<ApiResponse<AdminNews[], ErrorResponseModel>> => {
        try{
            const response = await api.get<AdminNews[]>('Admin/News/Get');
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