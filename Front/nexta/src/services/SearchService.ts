import ApiResponse from "../models/ApiResponse";
import api from "../http";
import PagedData from "../models/PagedData";
import Detail from "../models/Detail";
import SearchDetailRequest from "../models/search/SearchDetailRequest";

class SearchService{
    static async SearchDetail(request:SearchDetailRequest){
        const response = await api.post<ApiResponse<PagedData<Detail>>>('Search/SearchDetail', request);
        return response.data;
    }
}

export default SearchService;