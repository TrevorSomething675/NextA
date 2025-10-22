import { Product } from "../../../models/Product";

export interface OrderItem{
    orderId:string;
    count:number;
    productId:string;
    product:Product;
}