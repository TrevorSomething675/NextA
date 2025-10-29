import { Product } from "../../product/models/product";

export interface BasketItem{
    productId:string;
    userId:string;
    count:number;
    product?:Product;
}