import api from "../http/api";
import axios from 'axios';
import { ApiResponse } from "../http/BaseResponse";
import { ErrorResponseModel } from "../shared/models/ErrorResponseModel";
import { GetBasketProductsResponse } from "../http/models/basketProduct/GetBasketProducts";
import { AddBasketProductRequest, AddBasketProductResponse } from "../http/models/basketProduct/AddBasketProduct";
import { UpdateBasketProductRequest, UpdateBasketProductResponse } from "../http/models/basketProduct/UpdateBasketProduct";
import { DeleteBasketProductRequest, DeleteBasketProductResponse } from "../http/models/basketProduct/DeleteBasketProduct";

class BasketService{
    static async GetBasketProducts(userId:string) : Promise<ApiResponse<GetBasketProductsResponse, ErrorResponseModel>> {
        try {
            const response = await api.get<GetBasketProductsResponse>(`Basket/Get/${userId}`);
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

    static async AddBasketProduct(data: AddBasketProductRequest): Promise<ApiResponse<AddBasketProductResponse, ErrorResponseModel>> {
        try {
            const response = await api.post<AddBasketProductResponse>('Basket/Add', data);
            return {
                success: true,
                data: response.data,
                status: response.status
            };
        } catch (error) {
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

    static async DeleteBasketProduct(data:DeleteBasketProductRequest): Promise<ApiResponse<DeleteBasketProductResponse, ErrorResponseModel>> {
        try {
            const response = await api.delete<DeleteBasketProductResponse>('Basket/Delete', {
                params: {
                    userId: data.userId,
                    productId: data.productId
                }
            })
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
    static async UpdateBasketDetail(data:UpdateBasketProductRequest) : Promise<ApiResponse<UpdateBasketProductResponse, ErrorResponseModel>> {
        try {
            const response = await api.patch<UpdateBasketProductResponse>('Basket/Update', data)
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
            else{
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
}

export default BasketService;