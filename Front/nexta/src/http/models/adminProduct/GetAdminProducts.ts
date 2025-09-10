import { AdminProduct } from "../../../features/admin/models/AdminProduct"
import { PagedData } from "../../../shared/models/PagedDataT"

export interface GetAdminProductsRequest{
    searchTerm:string,
    pageNumber:number
}

export interface GetAdminProductsResponse{
    data: PagedData<AdminProduct>
}