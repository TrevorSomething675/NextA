import axios from 'axios';
import authStore from '../stores/AuthStore/authStore';
import { AuthService } from '../services/AuthService';

export const API_URL = 'https://localhost:7268';

const api = axios.create({
    withCredentials: true,
    baseURL: API_URL,
});

api.interceptors.request.use((config) => {
    if(config.headers){
        config.headers.Authorization = `Bearer ${localStorage.getItem('accessToken')}`;
        return config;
    }
});

api.interceptors.response.use(
    (response) => response,
    (error) => {
        if(error.response?.status === 403){
            authStore.isAdmin = false;

            window.location.href = '/error';
        }
        if(error.response?.status === 401){
            
            authStore.logout();
            AuthService.logout();
            
            window.location.href = '/auth';
        }
        return Promise.reject(error);
    }
)

export default api;