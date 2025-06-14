import DeleteBasketDetailRequest from "../models/basket/DeleteBasketDetailRequest";
import GetBasketDetailsRequest from "../models/basket/GetBasketDetailsRequest";
import AddBasketDetailRequest from "../models/basket/AddBasketDetailRequest";
import Detail from "../models/Detail";
import axios from "axios";
import api from "../http";
import GetBasketDetailsResponse from "../models/basket/GetBasketDetailsResponse";

class BasketService{
    static async GetBasketDetails(data:GetBasketDetailsRequest){
        try {
            const response = await api.post<GetBasketDetailsResponse>('Basket/Get', data)
            return response.data;
        } catch(error) {
            if(axios.isAxiosError(error) && error.response){
                return error.response.data as GetBasketDetailsResponse
            }
            else {
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
    static async AddBasketDetail(data:AddBasketDetailRequest){
        try {
            const response = await api.post<Detail>('Basket/Add', data)
            return response.data;
        } catch(error) {
            if(axios.isAxiosError(error) && error.response){
                return error.response.data as Detail
            } 
            else{
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
    static async DeletebasketDetail(data:DeleteBasketDetailRequest){
        try {
            const response = await api.post<Detail>('Basket/Delete', data)
            return response.data;
        } catch(error) {
            if(axios.isAxiosError(error) && error.response){
                return error.response.data as Detail
            } 
            else{
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
}

export default BasketService;