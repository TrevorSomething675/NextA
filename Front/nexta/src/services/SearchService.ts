import SearchDetailResponse from "../models/search/SearchDetailResponse";
import SearchDetailRequest from "../models/search/SearchDetailRequest";
import api from "../http/api";

class SearchService{
    static async SearchDetail(request:SearchDetailRequest){
        const response = await api.post<SearchDetailResponse>('Search/SearchDetail', request);
        return response.data;
    }
}

export default SearchService;