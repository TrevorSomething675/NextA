import { AdminProduct } from "../../../featuresLegacy/admin/models/AdminProduct"
import { PagedData } from "../../../sharedLegacy/models/PagedDataT"

export interface GetAdminProductsRequest{
    searchTerm:string,
    pageNumber:number,
    withCategory?:string
}

export interface GetAdminProductsResponse{
    data: PagedData<AdminProduct>
}