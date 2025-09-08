import { User } from "../../shared/entities/User";

export interface UserOrder {
    id:string,
    userId:string,
    user?: User,
    orderProducts:UserOrderProduct[]
    status:OrderStatus
    createdDate:string;
}

export interface UserOrderProduct{
    id:string
    name:string,
    article:string,
    description:string,
    newPrice:number,
    oldPrice:number,
    count:number
}

export enum OrderStatus{
	Unknown = -1, //Неизвестный статус
	Accepted = 0, //Принят
	InProgress = 1, //В работе
	Canceled = 2, //Отменён
	Ready = 3, //Готов к выдаче
	Complete = 4 //Завершён
}