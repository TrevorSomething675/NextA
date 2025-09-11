export interface UserBasketProduct{
    userId: string,
    productId: string,
    description:string,
    count: number,
    deliveryDate: string,
    article:string,
    name:string,
    status: UserBasketProductStatus,
    newPrice:number,
    oldPrice:number,
    imageBase64string: string
}

export enum UserBasketProductStatus{
    Unknown = -1, //Неизвестный статус
    Accepted = 0, //Принят
    AtWork = 1, //В работе
    Rejected = 2, //Отказ
    Waiting = 3 //Ожидает
}