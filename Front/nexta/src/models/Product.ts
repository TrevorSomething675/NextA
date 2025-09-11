import { Image } from "../shared/entities/Image";
import { UserOrder } from "./order/UserOrder";
import { UserBasketProduct } from "./UserBasketProduct";

export interface Product{
    id:string,
    name:string,
    article:string,
    description:string,
    status: ProductStatus,
    orderDate:string,
    deliveryDate:string,
    count:number,
    newPrice:number,
    oldPrice:number,
    basketProducts: UserBasketProduct[],
    orders:UserOrder[],
    image:Image
}

export enum ProductStatus{
    Unknown = -1, //Неизвестный статус
    InStock = 0, //Есть на складе
    OutOfStock = 1, //Нет на складе
}