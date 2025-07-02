import { Detail } from "../../../shared/entities/Detail"
import { PagedData } from "../../../shared/models/PagedDataT"

export interface GetAdminDetailsRequest{
    filter: GetAdminDetailsFilter
}

export interface GetAdminDetailsResponse{
    data:PagedData<Detail>
}

export interface GetAdminDetailsFilter{
    pageNumber:number,
    pageSize?:number,
    searchTerm?:string | null,
    withHidden?:boolean
}