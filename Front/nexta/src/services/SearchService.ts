import { SearchDetailsRequest, SearchDetailsResponse } from "../features/details/models/SearchDetails";
import api from "../http/api";

class SearchService{
    static async SearchDetail(request:SearchDetailsRequest){
        const response = await api.post<SearchDetailsResponse>('Search/SearchDetail', request);
        return response.data;
    }
}

export default SearchService;