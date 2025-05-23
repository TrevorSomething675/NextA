import Detail from "../Detail";
import PagedData from "../PagedData";

interface SearchDetailResponse{
    details:PagedData<Detail>
}

export default SearchDetailResponse;