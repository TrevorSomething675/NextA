import api from "../../../shared/http/api"
import { ApiResponse } from "../../../shared/http/models/BaseResponse"
import { ErrorResponseModel } from "../../../sharedLegacy/models/ErrorResponseModel"
import { PagedData } from "../../../sharedLegacy/models/PagedDataT";
import { Product } from "../models/product"
import axios from 'axios';

export class ProductApi{
    static GetById = async(id:string):Promise<ApiResponse<Product, ErrorResponseModel>> => {
        try{
            const response = await api.get<Product>(`Products/GetById/${id}`);
            return {
                success:true,
                data:response.data,
                status:response.status
            }
        }
        catch(error){
            if(axios.isAxiosError(error) && error.response){
                return {
                    success:false,
                    data:error.response.data as ErrorResponseModel,
                    status: error.response.status
                }
            }
            else{
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
    static GetAll = async(searchTerm:string = '', category:string = '', pageSize?:number, pageNumber?:number, withHidden: boolean = false,
        minPrice?:number, maxPrice?:number):Promise<ApiResponse<PagedData<Product>, ErrorResponseModel>> => {
            try{
                const response = await api.get('Products/Get',{
                    params: {
                        searchTerm,
                        pageSize,
                        pageNumber,
                        withHidden,
                        category,
                        minPrice,
                        maxPrice
                    }
                });
                return {
                    success:true,
                    data: response.data,
                    status: response.status
                }
            }
            catch(error){
                if(axios.isAxiosError(error) && error.response){
                    return {
                        success: false,
                        data: error.response.data as ErrorResponseModel,
                        status: error.response.status
                    };
                }
                else {
                    throw new Error('Сетевая ошибка или ошибка конфигурации');
                }
        }
    }
}