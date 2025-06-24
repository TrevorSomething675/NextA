import LoginForm from '../models/auth/Login';
import AuthModel from '../models/auth/AuthModel';
import RegisterForm from '../models/auth/Register';
import api from '../http';
import axios from 'axios';
import ErrorResponseModel from '../models/ErrorResponseModel';
import CheckRegisterUserResponse from '../models/auth/CheckRegisterUserResponse';
import CheckRegisterUserRequest from '../models/auth/checkRegisterUserRequest';

interface Tokens {
  accessToken?: string;
  refreshToken?: string;
}

class AuthService {
  private static readonly AUTH_ENDPOINTS = {
    LOGIN: 'Auth/login',
    REGISTER: 'Auth/Register',
    CHECKAUTH: 'Auth/CheckAuth',
    CHECKREGISTER: 'Auth/CheckRegisterUser'
  };

  static setAccessToken(accessToken:string){
    localStorage.setItem('AccessToken', accessToken);
  }

  private static setTokens({ accessToken, refreshToken }: Tokens): void {
    if (typeof window !== 'undefined') {
      localStorage.setItem('AccessToken', accessToken ?? '');
      localStorage.setItem('RefreshToken', refreshToken ?? '');
    }
  }

  private static clearTokens(): void {
    if (typeof window !== 'undefined') {
      localStorage.removeItem('AccessToken');
      localStorage.removeItem('RefreshToken');
    }
  }

  private static prepareFormData(data: Record<string, any>): FormData {
    const formData = new FormData();
    Object.entries(data).forEach(([key, value]) => {
      if (value !== undefined && value !== null) {
        formData.append(key, value);
      }
    });
    return formData;
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

  static async login(data: LoginForm): Promise<AuthModel> {
    try {
      const formData = this.prepareFormData({
        Email: data.email,
        Password: data.password,
      });

      const response = await api.post<AuthModel>(this.AUTH_ENDPOINTS.LOGIN, formData, {
        headers: { 'Content-Type': 'multipart/form-data' },
      });

      this.setTokens(response.data);
      return response.data;
    } catch (error) {
      this.handleError(error);
    }
  }

  static async checkRegisterUser(email:string){
    const request:CheckRegisterUserRequest = {
      email: email
    };
    const response = await api.post<CheckRegisterUserResponse>(this.AUTH_ENDPOINTS.CHECKREGISTER, request);
    return response.data;
  }

  static async register(data: RegisterForm): Promise<AuthModel> {
    try {
      const formData = this.prepareFormData({
        Email: data.email,
        FirstName: data.firstName,
        LastName: data.lastName,
        MiddleName: data.middleName,
        Password: data.password,
        ConfirmPassword: data.confirmPassword,
      });

      const response = await api.post<AuthModel>(this.AUTH_ENDPOINTS.REGISTER, formData, {
        headers: { 'Content-Type': 'multipart/form-data' },
      });

      this.setTokens(response.data);
      return response.data;
    } catch (error) {
      this.handleError(error);
    }
  }

  static async logout(){
    this.clearTokens();
  }

  static async checkAuth(userId:string, role:string) {
    const response = await api.post<AuthModel>(this.AUTH_ENDPOINTS.CHECKAUTH, 
      { 
        userId: userId,        
        role:role
      });
      console.log(response.data);
      this.setTokens(response.data);
    return response.data;
  }
}

export default AuthService;