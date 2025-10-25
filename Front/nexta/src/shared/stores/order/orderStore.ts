import { makeAutoObservable } from "mobx";
import { OrderItem } from "../../../entities/order/models/orderItem";

class OrderStore{
    items: OrderItem[] = [];

    constructor() {
        makeAutoObservable(this);
    }

    get totalOrderCount(){
        return this.items.length ?? 0;
    }

    setOrderItems = (newOrders: OrderItem[]) => {
        this.items = newOrders;
    }
}

export default new OrderStore();