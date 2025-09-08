import { ProductStatus } from "../../../../models/Product";

export interface UpdateAdminProductRequest{
    id:string,
    name:string,
    article:string,
    description:string,
    status:ProductStatus,
    count?:number,
    newPrice:number,
    oldPrice?:number,
    isVisible:boolean,
    type?:number,
    imageId?:string,
    imageName?:string,
    imageBase64String?:string
}

export interface UpdateAdminProductResponse{
    id:string,
    name:string,
    article:string,
    description:string,
    status:ProductStatus,
    count?:number,
    newPrice:number,
    oldPrice?:number,
    isVisible:boolean,
    imageId?:string,
    imageName?:string,
    imageBase64String?:string
}