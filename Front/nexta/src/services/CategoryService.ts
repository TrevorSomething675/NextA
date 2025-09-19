import api from "../http/api";
import axios from 'axios';
import { ApiResponse } from "../http/BaseResponse";
import { ErrorResponseModel } from "../shared/models/ErrorResponseModel";
import { GetCategoriesResponse } from "../http/models/categories/GetCategories";
import { AddCategoryRequest } from "../http/models/categories/AddCategory";
import { DeleteCategoryRequest } from "../http/models/categories/DeleteCategory";

class CategoryService{
    static async Delete(request: DeleteCategoryRequest) : Promise<ApiResponse<string, ErrorResponseModel>> {
        try {
            const response = await api.delete<string>('Admin/Categories/Delete', {
                params: {
                    name: request.name
                }
            });
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
    
    static async Get() : Promise<ApiResponse<GetCategoriesResponse, ErrorResponseModel>> {
        try {
            const response = await api.get<GetCategoriesResponse>('Categories/Get');
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

    static async Add(data: AddCategoryRequest) : Promise<ApiResponse<string, ErrorResponseModel>> {
        try {
            const response = await api.post<string>('Admin/Categories/Add', data);
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

export default CategoryService;