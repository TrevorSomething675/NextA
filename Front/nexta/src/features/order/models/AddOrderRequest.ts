import { Order } from "./Order"

export interface AddOrderRequest{
    userId:string,
    detailIds:string[]
}

export interface AddOrderResponse{
    order:Order
}