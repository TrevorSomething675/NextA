import Image from "../../../models/image/Image";
import UserDetail from "../../../models/UserDetail";
import { Order } from "../../order/models/Order";

export interface Detail{
    id:string,
    name:string,
    article:string,
    description:string,
    status:DetailStatus,
    orderDate:string,
    deliveryDate:string,
    count:number,
    newPrice:number,
    oldPrice:number,
    userDetails: UserDetail[],
    orders:Order[],
    image:Image
}

export enum DetailStatus{
    Unknown = -1, //Неизвестный статус
    InStock = 0, //Есть на складе
    OutOfStock = 1, //Нет на складе
}