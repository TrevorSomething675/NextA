import { ProductAttribute } from "./productAttribute";
import { ProductImage } from "./productImage";

export interface Product{
    name:string;
    article:string;
    description:string;
    status:ProductStatus;
    category:string;
    count:number;
    newPrice:number;
    oldPrice:number;
    isVisible:boolean;
    attributes:ProductAttribute[];
    images:ProductImage[];
}

export enum ProductStatus{
    Unknown = -1, //Неизвестный статус
    InStock = 0, //Есть на складе
    OutOfStock = 1, //Нет на складе
}