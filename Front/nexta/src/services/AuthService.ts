import api from "@/http";
import axios, { AxiosResponse } from "axios";
import ApiResponse from "@/models/auth/ApiResponse";
import AuthModel from "@/models/auth/AuthModel";
import RegisterForm from "@/models/auth/Register";
import LoginForm from "@/models/auth/Login";

class AuthService {
    static async login(data: LoginForm): Promise<ApiResponse<AuthModel>> {
        const formData = new FormData();

        formData.append('Email', data.email);
        formData.append('Password', data.password);

        try {
            const response = await api.post<ApiResponse<AuthModel>>('Auth/login', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            });
            return response.data;
        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                return error?.response.data;
            } else {
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }

    static async register(registerData:RegisterForm): Promise<AxiosResponse<ApiResponse<AuthModel>>>{
        const formData = new FormData();
        
        formData.append('Email', registerData.email);
        formData.append('FirstName', registerData.firstName);
        formData.append('LastName', registerData.lastName);
        formData.append('MiddleName', registerData.middleName);
        formData.append('Password', registerData.password);
        formData.append('ConfirmPassword', registerData.confirmPassword);

        const response = await api.post<ApiResponse<AuthModel>>('Auth/Register', formData, {headers:{
            'Content-Type': 'multipart/form-data'
        }});
        return response;
    }

    static async logout():Promise<void>{
        return api.post('/logout');
    }
}

export default AuthService;