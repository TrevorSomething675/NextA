import { ApiResponse } from "../http/BaseResponse";
import api from "../http/api";
import axios from 'axios';
import { ErrorResponseModel } from "../shared/models/ErrorResponseModel";
import { GetNotificationsResponse } from "../http/models/notifications/GetNotifications";

class NotificationService{
    static async Get(userId:string, pageSize?:number, pageNumber?:number) : Promise<ApiResponse<GetNotificationsResponse, ErrorResponseModel>>{
        try{
            const response = await api.get(`Notifications/Get/${userId}`, {
                params: {
                    userId: userId,
                    pageSize: pageSize,
                    pageNumber: pageNumber
                }
            });
            return {
                success: true,
                data: response.data,
                status: response.status
            }
        } catch(error){
            if(axios.isAxiosError(error) && error.response){
                return {
                    success: false,
                    data: error.response.data as ErrorResponseModel,
                    status: error.response.status
                };
            } else {
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
}

export default NotificationService;