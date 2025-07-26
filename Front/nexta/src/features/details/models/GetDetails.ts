import { Detail } from "../../../shared/entities/Detail"
import { PagedData } from "../../../shared/models/PagedDataT"

export interface GetDetailsRequest{
    filter: GetDetailsFilter
}

export interface GetDetailsResponse{
    data:PagedData<Detail>
}

export interface GetDetailsFilter{
    pageNumber:number,
    pageSize?:number,
    searchTerm?:string | null
}