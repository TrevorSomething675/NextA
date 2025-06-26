import { Detail } from "./Detail";

export interface UserDetail{
    detail: Detail,
    count: number,
    deliveryDate: string,
    status: UserDetailStatus
}

export enum UserDetailStatus{
    Unknown = -1, //Неизвестный статус
    Accepted = 0, //Принят
    AtWork = 1, //В работе
    Rejected = 2, //Отказ
    Waiting = 3 //Ожидает
}