import { UserOrder } from "../../../models/order/UserOrder"
import { PagedData } from "../../../sharedLegacy/models/PagedDataT"

export interface GetOrdersForUserResponse {
    data:PagedData<UserOrder>,
    totalCount:number
}