import { Order } from "../../../shared/entities/Order"
import { PagedData } from "../../../shared/models/PagedDataT"

export interface GetOrdersForUserFilter{
    userId:string,
    pageSize:number,
    pageNumber:number
}

export interface GetOrdersForUserRequest{
    filter:GetOrdersForUserFilter
}

export interface GetOrdersForUserResponse {
    data:PagedData<Order>
    totalCount:number
}