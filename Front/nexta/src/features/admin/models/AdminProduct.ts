import { ProductStatus } from "../../../models/Product"
import { Image } from "../../../shared/entities/Image"
import { ProductAttribute } from "../../../shared/entities/ProductAttribute"

export interface AdminProduct {
    id:string,
    name:string,
    article:string,
    description:string,
    status:ProductStatus,
    orderDate:string,
    deliveryDate:string,
    category:string,
    count:number,
    newPrice:number,
    oldPrice:number,
    isVisible:boolean,
    image:Image,
    attributes:ProductAttribute[]
}