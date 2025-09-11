import { makeAutoObservable } from "mobx";
import { UserOrder } from "../models/order/UserOrder";

class OrderStore{
    items: UserOrder[] = [];

    constructor() {
        makeAutoObservable(this);
    }

    get totalOrderCount(){
        return this.items.length ?? 0;
    }

    setOrderItems = (newOrders: UserOrder[]) => {
        this.items = newOrders;
    }
}

export default new OrderStore();