import { OrderDetail } from "../../../../shared/entities/OrderDetail";

export interface UpdateAdminOrderRequest{
    id:string,
    orderDetails:OrderDetail[],
    status:number
}