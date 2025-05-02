import GetBasketDetailsRequest from "@/models/basket/GetBasketDetailsRequest";
import ApiResponse from "@/models/auth/ApiResponse";
import Detail from "@/models/Detail";
import api from "@/http";

class BasketService{
    static async GetBasketDetails(data:GetBasketDetailsRequest){
        let response = api.post<ApiResponse<Detail[]>>('Basket/GetBasketDetails', data)
        return response;
    }
}

export default BasketService;