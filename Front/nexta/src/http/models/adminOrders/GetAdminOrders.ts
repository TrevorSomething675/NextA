import { UserOrder } from "../../../models/order/UserOrder";
import { PagedData } from "../../../sharedLegacy/models/PagedDataT";

export interface GetAdminOrdersRequest {
    statuses: number[],
    searchTerm:string,
    pageNumber:number,
    pageSize:number
}

export interface GetAdminOrdersResponse {
    data:PagedData<UserOrder>
}