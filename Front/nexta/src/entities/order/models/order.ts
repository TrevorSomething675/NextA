import { User } from "../../user/models/user";
import { OrderItem } from "./orderItem";

export interface Order{
	id:string;
    userId:string;
    status:OrderStatus;
    createdDate:string;
    products:OrderItem[];
	user:User;
}

export enum OrderStatus{
	Unknown = -1, //Неизвестный статус
	Accepted = 0, //Принят
	InProgress = 1, //В работе
	Canceled = 2, //Отменён
	Ready = 3, //Готов к выдаче
	Complete = 4 //Завершён
}