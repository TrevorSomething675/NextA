import api from "../http/api";
import axios from 'axios';
import { GetProductsResponse } from "../http/models/product/GetProducts";
import { ErrorResponseModel } from "../shared/models/ErrorResponseModel";
import { GetProductResponse } from "../http/models/product/GetProduct";
import { ApiResponse } from "../http/BaseResponse";

class ProductsService{
    static async Get(searchTerm:string = '', pageSize?:number, pageNumber?:number, withHidden: boolean = false) : Promise<ApiResponse<GetProductsResponse, ErrorResponseModel>>{
        try{
            const response = await api.get<GetProductsResponse>('Products/Get', {
                params: {
                    searchTerm,
                    pageSize,
                    pageNumber,
                    withHidden
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
    static async GetById(id:string) : Promise<ApiResponse<GetProductResponse, ErrorResponseModel>>{
        try{
            const response = await api.get<GetProductResponse>(`Products/GetById/${id}`);
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
            }
            else{
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
}

export default ProductsService;