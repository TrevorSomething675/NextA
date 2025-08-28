export interface BasketProduct{
    userId: string,
    productId: string,
    count: number,
    deliveryDate: string,
    article:string,
    name:string,
    status: BasketProductStatus,
    newPrice:number,
    oldPrice:number
}

export enum BasketProductStatus{
    Unknown = -1, //Неизвестный статус
    Accepted = 0, //Принят
    AtWork = 1, //В работе
    Rejected = 2, //Отказ
    Waiting = 3 //Ожидает
}