import { Product } from "../product/product";

export interface OrderItem{
    orderId?:string;
    count:number;
    productId:string;
    product?:Product;
}