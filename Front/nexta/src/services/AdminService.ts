import { AddAdminImageRequest } from "../features/admin/models/AddAdminImageRequest";
import { AddAdminImageResponse } from "../features/admin/models/AddAdminImageResponse";
import { AddNewsForm } from "../features/admin/models/AddNews/AddNews";
import { AdminNewsResponse } from "../features/admin/models/News/AdminNewsResponse";
import api from "../http/api";
import axios from 'axios';
import { ErrorResponseModel } from "../shared/models/ErrorResponseModel";
import { GetAdminProductResponse } from "../features/admin/models/AdminProduct/GetAdminProduct";
import { GetAdminProductsRequest, GetAdminProductsResponse } from "../features/admin/models/AdminProduct/GetAdminProducts";
import { ApiResponse } from "../http/BaseResponse";
import { UpdateAdminProductRequest, UpdateAdminProductResponse } from "../features/admin/models/AdminProduct/UpdateAdminProduct";
import { CreateAdminProductRequest, CreateAdminProductResponse } from "../features/admin/models/AdminProduct/CreateAdminProduct";

class AdminService{
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

    static async AddImageForDetail(request: AddAdminImageRequest){
        try{
            const response = await api.post<AddAdminImageResponse>('Admin/Image/Add', request);
            return response.data;
        } catch(error){
            if(axios.isAxiosError(error) && error.response){
                return error.response.data as AddAdminImageResponse
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

    static async AddNews(request: AddNewsForm){
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