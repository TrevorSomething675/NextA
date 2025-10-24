import axios from 'axios';
import api from '../../../shared/http/api';
import { ErrorResponseModel } from '../../../sharedLegacy/models/ErrorResponseModel';
import { ApiResponse } from '../../../shared/http/models/BaseResponse';

export class AuthApi{
    static async AccessRecovery(email:string, code:string, password:string, confirmPassword:string) : Promise<ApiResponse<string, ErrorResponseModel>>{
        try {
            const request = {
                email,
                code,
                password,
                confirmPassword
            };
            const response = await api.post('Accounts/AccessRecovery', request);
            
            return {
                success: true,
                data: response.data,
                status: response.status
            }
        }
        catch(error) {
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