import { makeAutoObservable } from "mobx";
import Order from "../models/Order";

class OrderStore{
    orders: Order[] = [];
    totalOrders:number = 0;

    constructor() {
        makeAutoObservable(this);
    }

    setTotalOrders = (totalOrders:number) =>{
        this.totalOrders = totalOrders;
    }

    setOrders = (orders:Order[]) => {
        this.orders = orders;
    }
}

export default new OrderStore();