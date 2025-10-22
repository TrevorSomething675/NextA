import { BasketItem } from "./basketItem";

export interface Basket{
    userId:string;
    products:BasketItem;
}