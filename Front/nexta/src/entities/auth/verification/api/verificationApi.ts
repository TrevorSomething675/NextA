import { ErrorResponseModel } from "../../../../shared/models/ErrorResponseModel";
import { ApiResponse } from "../../../../shared/http/models/BaseResponse";
import { AuthUser } from "../../../../stores/AuthStore/models/AuthUser";
import api from "../../../../shared/http/api";
import axios from 'axios';

export class VerificationApi{
    static SendVerificationCode = async(email:string):Promise<ApiResponse<null, ErrorResponseModel>> => {
        const request = {
            email
        };
        try{
            const response = await api.post('Code/SendVerificationCode', request);
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
    static VerifyCode = async(email:string, code:string, role:string):Promise<ApiResponse<AuthUser, ErrorResponseModel>> => {
        try{
            const request = {
                email,
                code,
                role
            }
            const response = await api.post('Code/VerifyCode', request);
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