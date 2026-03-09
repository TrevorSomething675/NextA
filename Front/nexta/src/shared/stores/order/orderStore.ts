import { makeAutoObservable } from "mobx";
import { Order } from "../../../entities/order/order";

class OrderStore{
    items: Order[] = [];

    constructor() {
        makeAutoObservable(this);
    }

    get totalOrderCount(){
        return this.items.length ?? 0;
    }

    setOrderItems = (newOrders: Order[]) => {
        this.items = newOrders;
    }
}

export default new OrderStore();