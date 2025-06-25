import PagedData from "../../../models/PagedData"
import { Detail } from "./Detail"

export interface GetDetailsRequest{
    filter: GetDetailsFilter
}

export interface GetDetailsResponse{
    details:PagedData<Detail>
}

export interface GetDetailsFilter{
    pageNumber:number,
    pageSize?:number,
    searchTerm?:string | null
}