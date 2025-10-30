import api from "../api";
import { ApiResponse } from "../models/BaseResponse";
import { ErrorResponseModel } from "../../models/ErrorResponseModel";
import { PagedData } from "../../models/PagedDataT";
import axios from 'axios';
import { Notification } from "../../../entities/user/models/notification";

export class NotificationApi {
    static async Get(userId:string, pageSize?:number, pageNumber?:number) : Promise<ApiResponse<PagedData<Notification>, ErrorResponseModel>> {
        try{
            const response = await api.get(`Notifications/Get/${userId}`, {
                params:{
                    userId: userId,
                    pageSize: pageSize,
                    pageNumber: pageNumber
                }
            });
            return {
                success:true,
                data:response.data,
                status:response.status
            }
        }
        catch(error) {
            if (axios.isAxiosError(error) && error.response) {
                return { 
                    success: false,
                    data: error.response.data as ErrorResponseModel,
                    status: error.response.status
                };
            }
            throw new Error('Сетевая ошибка или ошибка конфигурации');
        }
    }
}