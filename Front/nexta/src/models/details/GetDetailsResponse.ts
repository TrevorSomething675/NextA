import Detail from "../Detail";
import PagedData from "../PagedData";

interface GetDetailsResponse{
    pagedDetails: PagedData<Detail>
}

export default GetDetailsResponse;