import { AdminNewsResponse } from "../http/models/adminNews/AdminNews";
import api from "../http/api";
import axios from 'axios';
import { ErrorResponseModel } from "../shared/models/ErrorResponseModel";
import { ApiResponse } from "../http/BaseResponse";
import { GetAdminProductsRequest, GetAdminProductsResponse } from "../http/models/adminProduct/GetAdminProducts";
import { GetAdminProductResponse } from "../http/models/adminProduct/GetAdminProduct";
import { UpdateAdminProductRequest, UpdateAdminProductResponse } from "../http/models/adminProduct/UpdateAdminProduct";
import { CreateAdminProductRequest, CreateAdminProductResponse } from "../http/models/adminProduct/CreateAdminProduct";
import { AddNewsRequest } from "../http/models/news/AddNews";
import { GetUsersResponse } from "../http/models/users/GetUsers";

class AdminService{
    static async userDelete(userId:string) : Promise<ApiResponse<string, ErrorResponseModel>> {
        try {
            const response = await api.delete<string>(`Admin/Users/Delete/${userId}`);
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

    static async GetUsers(searchTerm?:string, pageNumber?:number, pageSize?:number) : Promise<ApiResponse<GetUsersResponse, ErrorResponseModel>>{
        try{
            const response = await api.get<GetUsersResponse>(`Admin/Users/Get`,{
                params: {
                    searchTerm: searchTerm ?? '',
                    pageNumber: pageNumber,
                    pageSize
                }
            });
            return {
                success: true,
                data: response.data,
                status: response.status
            } 
        } catch(error){
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

    static async GetAdminProduct(id:string) : Promise<ApiResponse<GetAdminProductResponse, ErrorResponseModel>>{
        try{
            const response = await api.get<GetAdminProductResponse>(`Admin/Products/GetById/${id}`);
            return {
                success: true,
                data: response.data,
                status: response.status
            } 
        } catch(error){
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

    static async GetAdminProducts(request:GetAdminProductsRequest) : Promise<ApiResponse<GetAdminProductsResponse, ErrorResponseModel>>{
        try{
            const response = await api.get<GetAdminProductsResponse>('Admin/Products/Get', {
                params: {
                    searchTerm: request.searchTerm ?? '',
                    pageNumber: request.pageNumber
                }
            });
            return {
                success: true,
                data: response.data,
                status: response.status
            }
        } catch(error) {
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

    static async UpdateAdminProduct(request: UpdateAdminProductRequest) : Promise<ApiResponse<UpdateAdminProductResponse, ErrorResponseModel>>{
        try{
            const response = await api.patch<UpdateAdminProductResponse>('Admin/Products/Update', request);
            return {
                success:true,
                data: response.data,
                status: response.status
            }
        } catch(error){
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

    static async CreateAdminProduct(request: CreateAdminProductRequest) : Promise<ApiResponse<CreateAdminProductResponse, ErrorResponseModel>>{
        try{
            const response = await api.post<CreateAdminProductResponse>('Admin/Products/Add', request);
            return {
                success: true,
                data: response.data,
                status: response.status
            }
        } catch(error){
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

    static async AddNews(request: AddNewsRequest){
        try{
            const response = await api.post<AdminNewsResponse>('Admin/News/Add', request);
            return response.data;
        } catch(error){
            if(axios.isAxiosError(error) && error.response){
                throw error.response.data as ErrorResponseModel;
            } else {
                throw error;
            }
        }
    }
}

export default AdminService;