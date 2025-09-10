import { CheckAuthRequest, CheckAuthResponse } from "../http/models/auth/CheckAuth";
import { IsRegisteredUserResponse } from "../http/models/auth/IsRegister";
import { LoginRequest, LoginResponse } from "../http/models/auth/Login";
import { RegistrationRequest, RegistrationResponse } from "../http/models/auth/Registration";
import { VerifyCodeRequest, VerifyCodeResponse } from "../http/models/auth/VerifyCode";
import api from "../http/api";
import { ApiResponse } from "../http/BaseResponse";
import { ErrorResponseModel } from "../shared/models/ErrorResponseModel";
import axios from 'axios';

export class AuthService{
    private static readonly AUTH_ENPOINTS = {
        LOGIN: 'Auth/login',
        REGISTER: 'Auth/Register',
        CHECKAUTH: 'Auth/CheckAuth',
        ISREGISTERUSER: 'Auth/IsRegisterUser',
        SENDVERIFICATIONCODE: 'Code/SendVerificationCode',
        VERIFYCODE: 'Code/VerifyCode',
        CHANGEPASSWORD: 'Account/ChangePassword',
        CHANGEPHONE: 'Account/ConfirmPhone'
    }

    static async login(data: LoginRequest) : Promise<ApiResponse<LoginResponse, ErrorResponseModel>>{
        try {
            const response = await api.post<LoginResponse>(this.AUTH_ENPOINTS.LOGIN, data);

            if(response.status === 200){
                localStorage.setItem('userId', response.data.user.id ?? '');
                localStorage.setItem('email', response.data.user.email ?? '');
                localStorage.setItem('firstName', response.data.user.firstName ?? '');
                localStorage.setItem('middleName', response.data.user.middleName ?? '');
                localStorage.setItem('lastName', response.data.user.lastName ?? '');
                localStorage.setItem('phone', response.data.user.phone?.toString() ?? '');
            }

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
    
    static async register(data:RegistrationRequest): Promise<ApiResponse<RegistrationResponse, ErrorResponseModel>>{
        try{
            const response = await api.post<RegistrationResponse>(this.AUTH_ENPOINTS.REGISTER, data);
            
            localStorage.setItem('userId', response.data.user.id ?? '');
            localStorage.setItem('email', response.data.user.email ?? '');
            localStorage.setItem('firstName', response.data.user.firstName ?? '');
            localStorage.setItem('middleName', response.data.user.middleName ?? '');
            localStorage.setItem('lastName', response.data.user.lastName ?? '');
            localStorage.setItem('phone', response.data.user.phone?.toString() ?? '');
            localStorage.setItem('isAuth', 'true');
            localStorage.setItem('accessToken', response.data.accessToken);

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

    static logout = () => {
        localStorage.removeItem('userId');
        localStorage.removeItem('email');
        localStorage.removeItem('firstName');
        localStorage.removeItem('middleName');
        localStorage.removeItem('lastName');
        localStorage.removeItem('phone');
        localStorage.removeItem('accessToken');
        localStorage.removeItem('isAuth');
        localStorage.removeItem('role');
    }

    static async sendVerificationCode(email:string) : Promise<ApiResponse<null, ErrorResponseModel>>{
        try{
            const response = await api.post(this.AUTH_ENPOINTS.SENDVERIFICATIONCODE, {email});
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
    static async verifyCode(request:VerifyCodeRequest) : Promise<ApiResponse<VerifyCodeResponse, ErrorResponseModel>>{
        try{
            const response = await api.post(this.AUTH_ENPOINTS.VERIFYCODE, request);

            localStorage.setItem('accessToken', response.data.accessToken);
            localStorage.setItem('isAuth', "true");

            return{
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
    static async isRegisterUser(email:string) : Promise<ApiResponse<IsRegisteredUserResponse, ErrorResponseModel>>{
        try{
            const response = await api.get<IsRegisteredUserResponse>(this.AUTH_ENPOINTS.ISREGISTERUSER, {
                params: {
                    email
                }
            });
            return{
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
    static async checkAuth(){
        try{
            const request: CheckAuthRequest = {
                email: localStorage.getItem('email') ?? '',
                role: localStorage.getItem('role') ?? 'User'
            }

            const response = await api.post<CheckAuthResponse>(this.AUTH_ENPOINTS.CHECKAUTH, request);
            if(response.status === 200){
                localStorage.setItem('userId', response.data.user.id ?? '');
                localStorage.setItem('email', response.data.user.email ?? '');
                localStorage.setItem('firstName', response.data.user.firstName ?? '');
                localStorage.setItem('middleName', response.data.user.middleName ?? '');
                localStorage.setItem('lastName', response.data.user.lastName ?? '');
                localStorage.setItem('phone', response.data.user.phone?.toString() ?? '');
                localStorage.setItem('accessToken', response.data.accessToken);
                localStorage.setItem('isAuth', 'true');
                localStorage.setItem('role', response.data.user.role ?? '');
            }

            return{
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