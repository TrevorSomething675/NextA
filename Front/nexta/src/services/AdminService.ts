import { GetAdminDetailRequest, GetAdminDetailResponse } from "../features/admin/models/GetAdminDetail";
import { GetAdminDetailsRequest, GetAdminDetailsResponse } from "../features/admin/models/GetAdminDetails";
import api from "../http/api";
import GetAllOrdersRequest from "../models/admin/GetAllOrders/GetAllOrdersRequest";
import GetAllOrdersResponse from "../models/admin/GetAllOrders/GetAllOrdersResponse";
import axios from 'axios';

class AdminService{
    static async GetAllOrders(request:GetAllOrdersRequest){
        try{
            const response = await api.post<GetAllOrdersResponse>('Admin/Home/GetAllOrders', request);
            return response.data;
        } catch(error){
            throw new Error('Сетевая ошибка или ошибка конфигурации');
        }
    }

    static async GetAdminDetail(id:string){
        try{
            const GetAdminDetailRequest:GetAdminDetailRequest = {
                detailId: id
            };
            const response = await api.post<GetAdminDetailResponse>('Admin/Detail/Get', GetAdminDetailRequest)
            return response.data;
        } catch(error){
            throw new Error('Сетевая ошибка или ошибка конфигурации');
        }
    }

    static async GetAdminDetails(request:GetAdminDetailsRequest){
        try{
            const response = await api.post<GetAdminDetailsResponse>('Admin/Detail/GetAll', request);
            return response.data
        } catch(error){
            if(axios.isAxiosError(error) && error.response){
                return error.response.data as GetAdminDetailsResponse
            } else {
                throw new Error('Сетевая ошибка или ошибка конфигурации');
            }
        }
    }
}

export default AdminService;