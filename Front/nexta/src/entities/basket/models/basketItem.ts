import { Product } from "../../../models/Product";

export interface BasketItem{
    productId:string;
    userId:string;
    product?:Product;
}