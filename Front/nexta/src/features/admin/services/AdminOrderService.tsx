import GetAllOrdersResponse from "../../../models/admin/GetAllOrders/GetAllOrdersResponse";
import GetAllOrdersRequest from "../../../models/admin/GetAllOrders/GetAllOrdersRequest";
import api from "../../../http/api";
import { DeleteDetailFromOrderRequest } from "../models/DeleteDetailFromOrder/DeleteDetailFromOrderRequest";
import { DeleteDetailFromOrderResponse } from "../models/DeleteDetailFromOrder/DeleteDetailFromOrderResponse";
import { UpdateAdminOrderResponse } from "../models/UpdateOrder.ts/UpdateAdminOrderResponse";
import { UpdateAdminOrderRequest } from "../models/UpdateOrder.ts/UpdateAdminOrderRequest";

class AdminOrderService{
    static async DeleteDetailFromOrder(orderId:string, detailId:string){
        try{
            const request:DeleteDetailFromOrderRequest = {orderId, detailId};
            const response = await api.post<DeleteDetailFromOrderResponse>('Admin/Order/Delete', request);
            return response.data;
        } catch(error){
            throw new Error('Сетевая ошибка или ошибка конфигурации');
        }
    }

    static async GetAllOrders(request:GetAllOrdersRequest){
        try{
            const response = await api.post<GetAllOrdersResponse>('Admin/Order/GetAllOrders', request);
            return response.data;
        } catch(error){
            throw new Error('Сетевая ошибка или ошибка конфигурации');
        }
    }

    static async UpdateOrder(request:UpdateAdminOrderRequest){
        try{
            const response = await api.post<UpdateAdminOrderResponse>('Admin/Order/Update', request);
            return response.data;
        } catch(error){
            throw new Error('Сетевая ошибка или ошибка конфигурации');
        }
    }
}

export default AdminOrderService;