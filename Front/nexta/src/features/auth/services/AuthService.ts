import { LoginRequest, LoginResponse } from "../models/login";
import api from "../../../http/api";
import axios from 'axios';
import { ErrorResponseModel } from "../../../shared/models/ErrorResponseModel";
import { RegistrationRequest, RegistrationResponse } from "../models/registration";
import { IsRegisteredUserResponse } from "../models/isRegister";
import { IsAuthRequest, IsAuthResponse } from "../models/isAuth";
import { VerifyCodeRequest } from "../models/verifyCode";
import { ChangePasswordRequest } from "../models/changePassword";
import { ConfirmPhoneFormRequest, ConfirmPhoneFormResponse } from "../../account/models/ConfirmPhoneForm";
import { ApiResponse } from "../../../http/BaseResponse";

export class AuthService{
    private static readonly AUTH_ENPOINTS = {
        LOGIN: 'Auth/login',
        REGISTER: 'Auth/Register',
        CHECKAUTH: 'Auth/IsAuth',
        CHECKREGISTER: 'Auth/IsRegisterUser',
        SENDVERIFICATIONCODE: 'Code/SendVerificationCode',
        VERIFYCODE: 'Code/VerifyCode',
        CHANGEPASSWORD: 'Account/ChangePassword',
        CHANGEPHONE: 'Account/ConfirmPhone'
    }

    static async confirmPhone(data: ConfirmPhoneFormRequest): Promise<ApiResponse<ConfirmPhoneFormResponse, ErrorResponseModel>> {
        try{
            const formData = this.prepareFormData({
                email: data.email,
                phone: data.phone
            });
            const response = await api.post<ConfirmPhoneFormResponse>(this.AUTH_ENPOINTS.CHANGEPHONE, formData);

            return {
                success: true,
                data: response.data,
                status: response.status
            };
        }
        catch(error) {
            if(axios.isAxiosError(error) && error.response) {
                return {
                    success: false,
                    data: error.response.data as ErrorResponseModel,
                    status: error.response.status
                };
            }
            throw new Error('Сетевая ошибка или ошибка конфигурации');
        }
    }

    static async changePassword(data: ChangePasswordRequest): Promise<void> {
        try {
            const formData = this.prepareFormData({
                userId: data.userId,
                email: data.email,
                legacyPassword: data.legacyPassword,
                password: data.password,
                confirmPassword: data.confirmPassword
            });

            const response = await api.post<void>(this.AUTH_ENPOINTS.CHANGEPASSWORD, formData);
            return response.data;
        }
        catch(error) {
            this.handleError(error);
        }
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
            
            if(response.status === 200) {
                localStorage.setItem('userId', response.data.user.id ?? '');
                localStorage.setItem('email', response.data.user.email ?? '');
                localStorage.setItem('firstName', response.data.user.firstName ?? '');
                localStorage.setItem('middleName', response.data.user.middleName ?? '');
                localStorage.setItem('lastName', response.data.user.lastName ?? '');
                localStorage.setItem('phone', response.data.user.phone?.toString() ?? '');
                localStorage.setItem('isAuth', 'true');
            }

            return response.data;
        }
        catch(error) {
            this.handleError(error);
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

    static async login(data: LoginRequest) : Promise<LoginResponse>{
        try {
            const formData = this.prepareFormData({
                email: data.email,
                password:data.password
            });

            const response = await api.post<LoginResponse>(this.AUTH_ENPOINTS.LOGIN, formData);

            if(response.status === 200){
                localStorage.setItem('userId', response.data.user.id ?? '');
                localStorage.setItem('email', response.data.user.email ?? '');
                localStorage.setItem('firstName', response.data.user.firstName ?? '');
                localStorage.setItem('middleName', response.data.user.middleName ?? '');
                localStorage.setItem('lastName', response.data.user.lastName ?? '');
                localStorage.setItem('phone', response.data.user.phone?.toString() ?? '');
            }

            return response.data;
        }
        catch(error) {
            this.handleError(error);
        }
    }

    static async verifyCode(request:VerifyCodeRequest){
        try{
            const response = await api.post(this.AUTH_ENPOINTS.VERIFYCODE, request);
            this.setAccessToken(response.data.accessToken);

            return response;
        }
        catch(error) {
            this.handleError(error);
        }
    }

    static async sendVerificationCode(email:string){
        try{
            const response = await api.post(this.AUTH_ENPOINTS.SENDVERIFICATIONCODE, {email});
            return response.data;
        }
        catch(error) {
            this.handleError(error);
        }
    }

    static async isAuth(){
        try{
            const request: IsAuthRequest = {
                email: localStorage.getItem('email') ?? '',
                role: localStorage.getItem('role') ?? 'User'
            }

            const response = await api.post<IsAuthResponse>(this.AUTH_ENPOINTS.CHECKAUTH, request);

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

            return response.data;
        }
        catch(error) {
            this.handleError(error);
        }
    }

    static async isRegisterUser(email:string) : Promise<IsRegisteredUserResponse>{
        try{
            const response = await api.post<IsRegisteredUserResponse>(this.AUTH_ENPOINTS.CHECKREGISTER, {email});
            return response.data;
        }
        catch(error) {
            this.handleError(error);
        }
    }

    private static setAccessToken(accessToken:string){
        localStorage.setItem('accessToken', accessToken);
        localStorage.setItem('isAuth', "true");
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