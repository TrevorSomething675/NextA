import api from "../http";
import GetAllOrdersRequest from "../models/admin/GetAllOrders/GetAllOrdersRequest";
import GetAllOrdersResponse from "../models/admin/GetAllOrders/GetAllOrdersResponse";

class AdminService{
    static async GetAllOrders(request:GetAllOrdersRequest){
        try{
            const response = await api.post<GetAllOrdersResponse>('Admin/Home/GetAllOrders', request);
            return response.data;
        } catch(error){
            throw new Error('Сетевая ошибка или ошибка конфигурации');
        }
    }
}

export default AdminService;