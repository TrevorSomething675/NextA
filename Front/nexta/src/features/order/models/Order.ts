import OrderDetail from "../../../models/OrderDetails";
import Detail from "../../../models/Detail";

export interface Order{
	id:string,
    userId:string,
    orderDetails:OrderDetail[],
	details:Detail[],
	status:OrderStatus
}

export enum OrderStatus{
	Unknown = -1, //Неизвестный статус
	Accepted = 0, //Принят
	InProgress = 1, //В работе
	Canceled = 2, //Отменён
	Ready = 3, //Готов к выдаче
	Complete = 4 //Завершён
}