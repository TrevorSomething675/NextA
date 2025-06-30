import { Order } from "../../../shared/entities/Order";
import { PagedData } from "../../../shared/models/PagedDataT";

interface GetAllOrdersResponse{
    data:PagedData<Order>
}

export default GetAllOrdersResponse;