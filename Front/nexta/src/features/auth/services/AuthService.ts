import { LoginRequest, LoginResponse } from "../models/login";
import api from "../../../http/api";
import axios from 'axios';
import ErrorResponseModel from "../../../models/ErrorResponseModel";
import { RegistrationRequest, RegistrationResponse } from "../models/registration";
import { IsRegisteredUserRequest, IsRegisteredUserResponse } from "../models/isRegister";
import { IsAuthRequest, IsAuthResponse } from "../models/isAuth";
import { SendVerificationCodeRequest } from "../models/sendVerificationCode";
import SendVerificationCodeResponse from "../../../models/code/sendVerificationCode/SendVerificationCodeResponse";
import VerifyCodeRequest from "../../../models/code/verifyCode/VerifyCodeRequest";
import VerifyCodeResponse from "../../../models/code/verifyCode/VerifyCodeResponse";

export class AuthService{
    private static readonly AUTH_ENPOINTS = {
        LOGIN: 'Auth/login',
        REGISTER: 'Auth/Register',
        CHECKAUTH: 'Auth/CheckAuth',
        CHECKREGISTER: 'Auth/CheckRegisterUser',
        SENDVERIFICATIONCODE: 'Code/SendVerificationCode',
        VERIFYCODE: 'Code/VerifyCode'
    }

    static async register(data:RegistrationRequest) : Promise<RegistrationResponse>{
        try{
            const formData = this.prepareFormData({
                email: data.email,
                firstName: data.firstName,
                middleName: data.middleName,
                lastName: data.lastName,
                password: data.password,
                confirmPassword: data.confirmPassword
            });

            const response = await api.post<RegistrationResponse>(this.AUTH_ENPOINTS.REGISTER, formData);
            return response.data;
        }
        catch(error){
            this.handleError(error);
        }
    }

    static async login(data: LoginRequest) : Promise<LoginResponse>{
        try {
            const formData = this.prepareFormData({
                email: data.email,
                password:data.password
            });

            const response = await api.post<LoginResponse>(this.AUTH_ENPOINTS.LOGIN, formData);
            return response.data;
        }
        catch(error){
            this.handleError(error);
        }
    }

    static async verifyCode(request:VerifyCodeRequest){
        try{
            const response = await api.post<VerifyCodeResponse>(this.AUTH_ENPOINTS.VERIFYCODE, request);
            this.setAcessToken(response.data.accessToken);
            
            return response.data;
        }
        catch(error){
            this.handleError(error);
        }
    }

    static async sendVerificationCode(email:string){
        try{
            const request:SendVerificationCodeRequest ={
                email:email
            }
            const response = await api.post<SendVerificationCodeResponse>(this.AUTH_ENPOINTS.SENDVERIFICATIONCODE, request);
            return response.data;
        }
        catch(error){
            this.handleError(error);
        }
    }

    static async isAuth(request:IsAuthRequest){
        try{
            const response = await api.post<IsAuthResponse>(this.AUTH_ENPOINTS.CHECKAUTH, request);
            return response.data;
        }
        catch(error){
            this.handleError(error);
        }
    }

    static async isRegisterUser(email:string) : Promise<IsRegisteredUserResponse>{
        try{
            const request:IsRegisteredUserRequest = {
                email: email
            };
            const response = await api.post<IsRegisteredUserResponse>(this.AUTH_ENPOINTS.CHECKREGISTER, request);
            return response.data;
        }
        catch(error){
            this.handleError(error);
        }
    }

    private static setAcessToken(accessToken:string){
        localStorage.setItem('AccessToken', accessToken);
    }

    private static handleError(error:unknown): never {
        if(axios.isAxiosError(error)) {
            if(error.response) {
                const responseData = error.response.data as | {message?: string; Message:string } | string;
                let message = 'error';

                if(typeof responseData === 'object' && responseData !== null) {
                    message = responseData.message || responseData.Message || message;
                } else if (typeof responseData === 'string') {
                    message = responseData;
                }

                const errorResponse: ErrorResponseModel = {
                    message,
                    statusCode: error.response.status
                }
                throw errorResponse;
            }
            if(error.request){
                throw{
                    message: 'Сервер не отвечает',
                    statusCode: 503
                } as ErrorResponseModel;
            }
        }
        throw {
            message: 'Сетевая ошибка',
            statusCode: 500
        } as ErrorResponseModel;
    }

    private static prepareFormData(data:Record<string, any>): FormData {
        const formData = new FormData();
        Object.entries(data).forEach(([key, value]) => {
            if (value !== undefined && value !== null) {
                formData.append(key, value);
            }
        });
        return formData;
    } 
}