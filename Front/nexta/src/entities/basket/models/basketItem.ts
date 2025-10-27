import { Product } from "../../../models/Product";

export interface BasketItem{
    productId:string;
    userId:string;
    count:number;
    product?:Product;
}