import { makeAutoObservable } from "mobx";
import { Order } from "../shared/entities/Order";

class OrderStore{
    orders: Order[] = [];
    totalCountOrders:number = 0;

    constructor() {
        makeAutoObservable(this);
    }

    setTotalCountOrders = (totalCountOrders:number) => {
        this.totalCountOrders = totalCountOrders;
    }

    setOrders = (orders:Order[]) => {
        this.orders = orders;
    }
}

export default new OrderStore();