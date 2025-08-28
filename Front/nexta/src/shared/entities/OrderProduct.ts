import { Product } from "../../models/Product";
import { Order } from "./Order";

export interface OrderProduct{
    count:number,
    orderId:string,
    order:Order,
    productId:string,
    product:Product
}