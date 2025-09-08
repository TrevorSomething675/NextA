import { ProductStatus } from "../../../models/Product"

export interface AdminProduct {
    id:string,
    name:string,
    article:string,
    description:string,
    status:ProductStatus,
    orderDate:string,
    deliveryDate:string,
    count:number,
    newPrice:number,
    oldPrice:number,
    isVisible:boolean,
    imageId:string,
    imageName:string,
    base64String:string
}