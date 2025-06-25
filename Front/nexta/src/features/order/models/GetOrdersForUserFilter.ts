import Order from "../../../models/Order"
import PagedData from "../../../models/PagedData"

export interface GetOrdersForUserFilter{
    userId:string,
    pageSize:number,
    pageNumber:number
}

export interface GetOrdersForUserRequest{
    filter:GetOrdersForUserFilter
}

export interface GetOrdersForUserResponse {
    orders:PagedData<Order>
    totalCount:number
}