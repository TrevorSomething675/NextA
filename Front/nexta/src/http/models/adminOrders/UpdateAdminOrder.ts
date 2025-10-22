import { OrderProduct } from "../../../sharedLegacy/entities/OrderProduct";

export interface UpdateAdminOrderRequest{
    id:string,
    userId:string,
    orderProducts:OrderProduct[],
    status:number
}

export interface UpdateAdminOrderResponse{
    id:string
}