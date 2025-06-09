import Detail from "./Detail";
import Order from "./Order";

interface OrderDetail{
    count:number,
    orderId:string,
    order:Order,
    detailId:string,
    detail:Detail
}

export default OrderDetail;