import { Image } from "../sharedLegacy/entities/Image";
import { ProductAttribute } from "../sharedLegacy/entities/ProductAttribute";
import { UserOrder } from "./order/UserOrder";
import { UserBasketProduct } from "./UserBasketProduct";

export interface Product{
    id:string,
    name:string,
    article:string,
    description:string,
    status: ProductStatus,
    category:string,
    count:number,
    oldPrice?:number,
    newPrice:number,
    basketProducts:UserBasketProduct[],
    orders:UserOrder[],
    image:Image
    attributes:ProductAttribute[]
}

export enum ProductStatus{
    Unknown = -1, //Неизвестный статус
    InStock = 0, //Есть на складе
    OutOfStock = 1, //Нет на складе
}