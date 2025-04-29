import axios from 'axios';

export const API_URL = 'https://localhost:7268';

const api = axios.create({
    withCredentials: true,
    baseURL: API_URL,
});

api.interceptors.request.use((config) => {
    config.headers.Authorization = `Bearer ${localStorage.getItem('AccessToken')}`;
    return config;
});

export default api;