import axios from 'axios';
import api from '../http/api';
import { ApiResponse } from '../http/BaseResponse';
import { ErrorResponseModel } from '../shared/models/ErrorResponseModel';
import { UpdateAccountRequest, UpdateAccountResponse } from '../http/models/account/UpdateAccount';
import { ConfirmUpdateEmailRequest, ConfirmUpdateEmailResponse } from '../http/models/account/ConfirmUpdateEmail';

class AccountService{
    static async updateEmail(data: ConfirmUpdateEmailRequest) : Promise<ApiResponse<ConfirmUpdateEmailResponse, ErrorResponseModel>> {
        try {
            const response = await api.patch<ConfirmUpdateEmailResponse>('Accounts/UpdateEmail', data);
            return {
                success: true,
                data: response.data,
                status: response.status
            };
        } catch(error) {
            if (axios.isAxiosError(error) && error.response) {
                return { 
                    success: false,
                    data: error.response.data as ErrorResponseModel,
                    status: error.response.status
                };
            }
            throw new Error('Сетевая ошибка или ошибка конфигурации');
        }
    }

    static async update(data: UpdateAccountRequest) : Promise<ApiResponse<UpdateAccountResponse, ErrorResponseModel>> {
        try {
            const response = await api.patch<UpdateAccountResponse>('Accounts/Update', data);
            return {
                success: true,
                data: response.data,
                status: response.status
            };
        } catch(error) {
            if (axios.isAxiosError(error) && error.response) {
                return { 
                    success: false,
                    data: error.response.data as ErrorResponseModel,
                    status: error.response.status
                };
            }
            throw new Error('Сетевая ошибка или ошибка конфигурации');
        }
    }
}

export default AccountService;