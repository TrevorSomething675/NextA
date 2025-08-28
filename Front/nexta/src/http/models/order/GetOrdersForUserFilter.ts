import { Order } from "../../../shared/entities/Order"
import { PagedData } from "../../../shared/models/PagedDataT"

export interface GetOrdersForUserRequest{
    userId:string,
    pageSize?:number,
    pageNumber?:number
}

export interface GetOrdersForUserResponse {
    data:PagedData<Order>,
    totalCount:number
}