import { PagedData } from "../../../../shared/models/PagedDataT";
import { AdminProduct } from "../AdminProduct";

export interface GetAdminProductsRequest{
    searchTerm:string,
    pageNumber:number
}

export interface GetAdminProductsResponse{
    data: PagedData<AdminProduct>
}