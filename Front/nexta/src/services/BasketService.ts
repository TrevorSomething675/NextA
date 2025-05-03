import GetBasketDetailsRequest from "../models/basket/GetBasketDetailsRequest";
import ApiResponse from "../models/ApiResponse";
import Detail from "../models/Detail";
import axios from "axios";
import api from "../http";
import AddBasketDetailRequest from "../models/basket/AddBasketDetailRequest";

class BasketService{
    static async GetBasketDetails(data:GetBasketDetailsRequest){
        try{
            const response = await api.post<ApiResponse<Detail[]>>('Basket/GetBasketDetails', data)
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
            const response = await api.post<ApiResponse<Detail>>('Basket/AddBasketDetailQueryRequest', data)
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