import { ProductStatus } from "../../../models/Product"
import { Image } from "../../../shared/entities/Image"

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
    image:Image
}