import GetBasketDetailsRequest from "../models/basket/GetBasketDetailsRequest";
import AddBasketDetailRequest from "../models/basket/AddBasketDetailRequest";
import ApiResponse from "../models/ApiResponse";
import Detail from "../models/Detail";
import axios from "axios";
import api from "../http";

class BasketService{
    static async GetBasketDetails(data:GetBasketDetailsRequest){
        try{
            const response = await api.post<ApiResponse<Detail[]>>('Basket/Get', data)
            return response.data;
        } catch(error) {
            if(axios.isAxiosError(error) && error.response){
                return error.response.data as ApiResponse<Detail[]>
            }
            else {
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
    static async AddBasketDetail(data:AddBasketDetailRequest){
        try{
            const response = await api.post<ApiResponse<Detail>>('Basket/Add', data)
            return response.data;
        } catch(error){
            if(axios.isAxiosError(error) && error.response){
                return error.response.data as ApiResponse<Detail[]>
            } 
            else{
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
}

export default BasketService;