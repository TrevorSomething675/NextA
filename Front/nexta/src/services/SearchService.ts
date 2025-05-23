import ApiResponse from "../models/ApiResponse";
import api from "../http";
import SearchDetailRequest from "../models/search/SearchDetailRequest";
import SearchDetailResponse from "../models/search/SearchDetailResponse";

class SearchService{
    static async SearchDetail(request:SearchDetailRequest){
        const response = await api.post<ApiResponse<SearchDetailResponse>>('Search/SearchDetail', request);
        return response.data;
    }
}

export default SearchService;