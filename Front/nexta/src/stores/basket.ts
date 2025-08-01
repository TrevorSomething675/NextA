import { makeAutoObservable } from "mobx";
import { Detail } from "../shared/entities/Detail";

class BasketStore {
    details: Detail[] = [];
    constructor() {
        makeAutoObservable(this);
    }
    
get totalPrice() {
    return this.details.reduce((sum, detail) => {
        const userDetails = Array.isArray(detail.userDetails) ? detail.userDetails : [];
        const userDetailsSum = userDetails.reduce((innerSum, userDet) => {
            return innerSum + (userDet?.count ?? 0) * (detail?.newPrice ?? 0);
        }, 0);
        return sum + userDetailsSum;
    }, 0);
    }
    
    setBasketDetails = (details: Detail[]) => {
        this.details = details;
    }

    deleteBasketDetail = (id: string) => {
        this.details = this.details.filter(detail => detail.id !== id);
    }

    addBasketDetail = (detail: Detail) => {
        this.details.push(detail);
    }

    clear = () =>{
        this.details = [];
    }
}

export default new BasketStore();