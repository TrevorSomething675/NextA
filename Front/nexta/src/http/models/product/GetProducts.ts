import { PagedData } from "../../../shared/models/PagedDataT";
import { Product } from "../../../models/Product";

export interface GetProductsRequest{
    searchTerm?:string | null,
    withHidden: boolean,
    pageNumber:number,
    pageSize?:number,
}

export interface GetProductsResponse{
    data: PagedData<Product>
}