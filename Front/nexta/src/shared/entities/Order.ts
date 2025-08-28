import { BasketProduct } from "../../models/basketProduct/BasketProduct";
import { Product } from "../../models/Product";
import { User } from "./User";

export interface Order{
	id:string,
    userId:string,
	user:User,
	basketProducts: BasketProduct[]
	products:Product[]
	status:OrderStatus,
	createdDate: string
}

export enum OrderStatus{
	Unknown = -1, //Неизвестный статус
	Accepted = 0, //Принят
	InProgress = 1, //В работе
	Canceled = 2, //Отменён
	Ready = 3, //Готов к выдаче
	Complete = 4 //Завершён
}