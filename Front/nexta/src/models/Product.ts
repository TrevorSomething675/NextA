import { Image } from "../shared/entities/Image";
import { ProductAttribute } from "../shared/entities/ProductAttribute";
import { UserOrder } from "./order/UserOrder";
import { UserBasketProduct } from "./UserBasketProduct";

export interface Product{
    id:string,
    name:string,
    article:string,
    description:string,
    category:string,
    status: ProductStatus,
    orderDate:string,
    deliveryDate:string,
    count:number,
    newPrice:number,
    oldPrice:number,
    basketProducts: UserBasketProduct[],
    orders:UserOrder[],
    image:Image
    attributes: ProductAttribute[]
}

export enum ProductStatus{
    Unknown = -1, //Неизвестный статус
    InStock = 0, //Есть на складе
    OutOfStock = 1, //Нет на складе
}