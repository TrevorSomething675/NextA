import { Order } from "../../../shared/entities/Order"

export interface CreateNewOrderRequest{
    userId:string,
    detailIds:string[]
}

export interface CreateNewOrderResponse{
    order:Order
}