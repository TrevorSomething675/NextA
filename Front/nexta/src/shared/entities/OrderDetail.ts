import { Detail } from "./Detail";
import { Order } from "./Order";

export interface OrderDetail{
    count:number,
    orderId:string,
    order:Order,
    detailId:string,
    detail:Detail
}