import { AdminUser } from "../../../models/AdminUser"
import { PagedData } from "../../../shared/models/PagedDataT"

export interface GetUsersRequest{
    searchTerm?:string | null,
    pageNumber:number,
    pageSize?:number
}

export interface GetUsersResponse{
    data: PagedData<AdminUser>
}