import { Detail } from "../../../shared/entities/Detail";
import { PagedData } from "../../../shared/models/PagedDataT";

export interface SearchDetailsResponse{
    details:PagedData<Detail>
}

export interface SearchDetailsRequest {
    filter:SearchDetailsFilter
}

export interface SearchDetailsFilter{
    searchTerm:string,
    pageNumber: number
}