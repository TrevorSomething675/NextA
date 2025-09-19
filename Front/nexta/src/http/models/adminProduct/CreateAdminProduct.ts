import { ProductStatus } from "../../../models/Product"

export interface CreateAdminProductRequest{
    name:string,
    article:string,
    description:string,
    status:ProductStatus,
    count:number,
    category?:string,
    newPrice:number,
    oldPrice?:number | null,
    isVisible:boolean,
    imageName?:string,
    ImageBase64String?:string
}

export interface CreateAdminProductResponse{
    id:string
}