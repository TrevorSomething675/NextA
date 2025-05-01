import Detail from "@/models/Detail";
import { action, makeObservable, observable } from "mobx";

class BasketStore{
    details: Detail[] = [];

    constructor(){
        makeObservable(this, {
            details: observable,
            setBasketDetails: action,
            deleteBasketDetail:action,
            addBasketDetaul:action
        })
    }

    setBasketDetails = (details:Detail[]) =>{
        this.details = details;
    }

    deleteBasketDetail = (id:string) =>{
        let updatedDetails = this.details.filter(detal => detal.id !== id);
        this.details = updatedDetails;
    }

    addBasketDetaul = (detail:Detail) =>{
        this.details.push(detail);
    }
}

export default new BasketStore();