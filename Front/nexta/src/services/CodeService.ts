import api from "../http/api";
import axios from 'axios';
import SendVerificationCodeRequest from "../models/code/sendVerificationCode/SendVerificationCodeRequest";
import SendVerificationCodeResponse from "../models/code/sendVerificationCode/SendVerificationCodeResponse";
import VerifyCodeResponse from "../models/code/verifyCode/VerifyCodeResponse";
import VerifyCodeRequest from "../models/code/verifyCode/VerifyCodeRequest";
import ErrorResponseModel from "../models/ErrorResponseModel";

class CodeService{
    static async SendVerificationCode(email:string){
        try{
            const request:SendVerificationCodeRequest = {
                email: email
            };
            const response = await api.post<SendVerificationCodeResponse>('Code/SendVerificationCode', request);
            return response.data
        } catch(error){
            if(axios.isAxiosError(error) && error.response){
                return error.response.data as SendVerificationCodeResponse
            } else {
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
    static async VerifyCode(request:VerifyCodeRequest){

        try{
            const formData = new FormData();
            formData.append("email", request.email ?? '');
            formData.append("userId", request.userId ?? '');
            formData.append("role", request.role ?? '');
            formData.append("code", request.code ?? '');
            
            const response = await api.post<VerifyCodeResponse>('Code/VerifyCode', formData);
            return response;
        }
        catch(error:any){
            this.handleError(error)
        }
    }
    private static handleError(error: unknown): never {
    if (axios.isAxiosError(error)) {
        if (error.response) {
        const responseData = error.response.data as 
            | { message?: string; Message?: string }
            | string;
        
        let message = 'Ошибка авторизации';
        
        if (typeof responseData === 'object' && responseData !== null) {
            message = responseData.message || responseData.Message || message;
        } else if (typeof responseData === 'string') {
            message = responseData;
        }
        
        const errorResponse: ErrorResponseModel = {
            message,
            statusCode: error.response.status
        };
        
        throw errorResponse;
        }
        
        if (error.request) {
        throw {
            message: 'Сервер не ответил',
            statusCode: 503
        } as ErrorResponseModel;
        }
    }
    
        throw {
        message: 'Сетевая ошибка или ошибка конфигурации',
        statusCode: 500
        } as ErrorResponseModel;
    }
}

export default CodeService;