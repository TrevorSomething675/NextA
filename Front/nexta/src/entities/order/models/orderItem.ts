import { Product } from "../../product/models/product";

export interface OrderItem{
    orderId:string;
    count:number;
    productId:string;
    product:Product;
}