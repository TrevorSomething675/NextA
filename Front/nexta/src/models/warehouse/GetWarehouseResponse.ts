import PagedData from "../PagedData";
import Detail from "../Detail";

interface GetWarehouseResponse {
    details: PagedData<Detail>
}

export default GetWarehouseResponse;