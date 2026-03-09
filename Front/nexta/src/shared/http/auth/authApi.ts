import axios from 'axios';
import api from '../api';
import { ErrorResponseModel } from '../../models/ErrorResponseModel';
import { ApiResponse } from '../models/BaseResponse';
import { User } from '../../../entities/user/user';
import { AuthData } from '../../../entities/auth/authData';
import { IsRegisterResponse } from './models/isRegisterResponse';

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

    static async ChangePassword(userId:string, email:string, 
        legacyPassword:string, password:string,
        confirmPassword:string
    ) : Promise<ApiResponse<string, ErrorResponseModel>>{
        try {
            const request = {
                userId,
                email,
                password,
                legacyPassword,
                confirmPassword
            }
            const response = await api.post('Accounts/ChangePassword', request);
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

    static async Login(email:string, password:string) : Promise<ApiResponse<User, ErrorResponseModel>>{
        try {
            const request = {
                email,
                password
            };
            const response = await api.post<User>('Auth/login', request);
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

    static async Register(email:string, firstName:string, middleName:string | undefined, 
        lastName:string, password:string, confirmPassword:string, code: string
    ): Promise<ApiResponse<AuthData, ErrorResponseModel>>{
        try{
            const request = {
                email,
                firstName,
                middleName,
                lastName,
                password,
                confirmPassword,
                code
            }
            const response = await api.post<AuthData>('Auth/Register', request);
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

    static async CheckAuth(email:string, role:string) : Promise<ApiResponse<AuthData, ErrorResponseModel>>{
        try{
            const request = {
                email,
                role
            };
            const response = await api.post<AuthData>('Auth/CheckAuth', request);
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
    
    static async IsRegister(email:string) : Promise<ApiResponse<IsRegisterResponse, ErrorResponseModel>>{
        try{
            const response = await api.get<IsRegisterResponse>('Auth/IsRegisterUser', {
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
}