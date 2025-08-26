import { Product } from "../product/Product";

export interface BasketProduct{
    product: Product,
    count: number,
    deliveryDate: string,
    status: BasketProductStatus
}

export enum BasketProductStatus{
    Unknown = -1, //Неизвестный статус
    Accepted = 0, //Принят
    AtWork = 1, //В работе
    Rejected = 2, //Отказ
    Waiting = 3 //Ожидает
}