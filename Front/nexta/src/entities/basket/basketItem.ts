import { Product } from "../product/product";

export interface BasketItem{
    productId:string;
    userId:string;
    count:number;
    product?:Product;
}