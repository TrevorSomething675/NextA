import axios from 'axios';
import auth from '../stores/auth';

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
        if(error.response?.status === 401){
            auth.logout();
            window.location.href = '/auth';
        }
        return Promise.reject(error);
    }
)

export default api;