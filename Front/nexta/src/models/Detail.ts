import UserDetail from "./UserDetail";

interface Detail{
    id:string,
    name:string,
    article:string,
    description:string,
    status:Status,
    orderDate:string,
    deliveryDate:string,
    count:number,
    newPrice:number,
    oldPrice:number,
    userDetail: UserDetail[]
}

enum Status{
    Unkown = -1, //Неизвестный статус
    Rejected = 0, //Отказ
    Accepted = 1, //Принят
    AtWork = 2, //В работе
    Waiting = 3 //Ожидает
}

export { Status };
export default Detail;