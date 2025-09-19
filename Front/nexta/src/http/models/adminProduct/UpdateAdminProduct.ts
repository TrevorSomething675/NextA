import { ProductStatus } from "../../../models/Product"
import { ProductAttribute } from "../../../shared/entities/ProductAttribute"

export interface UpdateAdminProductRequest{
    id:string,
    name:string,
    article:string,
    description:string,
    status:ProductStatus,
    count?:number,
    newPrice:number,
    category:string,
    oldPrice?:number,
    isVisible:boolean,
    type?:ProductOperationType,
    imageId?:string,
    imageName?:string,
    imageBase64String?:string,
    attributes:ProductAttribute[],
}

export enum ProductOperationType {
    Nothing = -1,
    Update = 0,
    Create = 1,
    Delete = 2
}

export interface UpdateAdminProductResponse{
    id:string,
    name:string,
    article:string,
    description:string,
    status:ProductStatus,
    count?:number,
    newPrice:number,
    category:string,
    oldPrice?:number,
    isVisible:boolean,
    imageId?:string,
    imageName?:string,
    imageBase64String?:string
}