import UserDetail from "./UserDetail";

interface Detail{
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
    userDetail: UserDetail[]
}

enum DetailStatus{
    Unkown = -1, //Неизвестный статус
    InStock = 0, //Есть на складе
    OutOfStock = 1, //Нет на складе
}

export { DetailStatus };
export default Detail;