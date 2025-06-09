import PagedData from "../PagedData";
import Order from "../Order";

interface GetOrdersForUserResponse{
    orders:PagedData<Order>,
    totalCount:number
}

export default GetOrdersForUserResponse;