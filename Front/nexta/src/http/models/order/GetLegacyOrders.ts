import { PagedData } from "../../../shared/models/PagedDataT"
import { Order } from "../../../shared/entities/Order"

export interface GetLegacyOrdersForUserRequest{
    userId:string,
    pageSize?:number,
    pageNumber?:number
}

export interface GetLegacyOrdersForUserResponse{
    data: PagedData<Order>
}