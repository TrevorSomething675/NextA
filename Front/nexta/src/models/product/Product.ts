import { Image } from "../../shared/entities/Image";
import { Order } from "../../shared/entities/Order";
import { BasketProduct } from "../basketProduct/BasketProduct";

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
    basketProducts: BasketProduct[],
    orders:Order[],
    image:Image
}

export enum ProductStatus{
    Unknown = -1, //Неизвестный статус
    InStock = 0, //Есть на складе
    OutOfStock = 1, //Нет на складе
}