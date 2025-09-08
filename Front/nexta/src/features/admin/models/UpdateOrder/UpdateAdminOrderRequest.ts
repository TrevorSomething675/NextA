import { OrderProduct } from "../../../../shared/entities/OrderProduct";

export interface UpdateAdminOrderRequest{
    id:string,
    userId:string,
    orderProducts:OrderProduct[],
    status:number
}