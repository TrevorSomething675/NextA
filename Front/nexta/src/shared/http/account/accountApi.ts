import { ErrorResponseModel } from "../../../sharedLegacy/models/ErrorResponseModel";
import { ApiResponse } from "../models/BaseResponse";
import { User } from "../../../entities/user/models/user";
import api from "../api";
import axios from 'axios';

export class AccountApi{
    static async UpdateEmail(legacyEmail:string, email:string, code:string):Promise<ApiResponse<User, ErrorResponseModel>> {
        try{
            const request = {
                email,
                legacyEmail,
                code
            };
            const response = await api.patch('Accounts/UpdateEmail', request);
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

    static async Update(id:string, firstName:string, lastName:string, middleName:string, email:string, phone:string):Promise<ApiResponse<User, ErrorResponseModel>> {
        try{
            const request = {
                id,
                firstName,
                lastName,
                middleName,
                email,
                phone
            }
            const response = await api.patch('Accounts/Update', request);
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