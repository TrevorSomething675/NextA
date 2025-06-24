import Order from "../../Order";
import PagedData from "../../PagedData";

interface GetAllOrdersResponse{
    orders:PagedData<Order>
}

export default GetAllOrdersResponse;