import { UserBasketProduct } from "../models/UserBasketProduct";
import { makeAutoObservable, runInAction } from "mobx";

class BasketStore {
    items: UserBasketProduct[] = [];
    isVisibleBasket = false;

    constructor() {
        makeAutoObservable(this);
    }

    private num(v: unknown, def = 0) {
        const n = Number(v);
        return Number.isFinite(n) ? n : def;
    }
    
    get totalPrice() {
        return this.items.reduce((sum, item) => {
            const price = this.num(item.newPrice, 0);
            const count = this.num(item.count, 0);
            return sum + price * count;
        }, 0);
    }

    get totalCount() {
        return this.items.reduce((sum, item) => sum + item.count, 0);
    }

    setVisibleBasket = (state:boolean) => {
        this.isVisibleBasket = state;
    }

    addBasketProduct = (product: UserBasketProduct) => {
        const existingItem = this.items.find(item => item.productId === product.productId)!;
        
        if (existingItem) {
        existingItem.count += 1;
        } else {
            this.items.push(product);
        }
    }

    deleteBasketProduct = (id: string) => {
        runInAction(() => {
            this.items = this.items.filter(i => i.productId !== id);
        });
    }

    changeProductCount = (id: string, newCount: number) => {
        const n = this.num(newCount, 1);
        const count = n > 0 ? Math.floor(n) : 1;
        const item = this.items.find(i => i.productId === id);
        if (item) item.count = count;
    };

    incrementCount = (id: string) => {
        const item = this.items.find(item => item.productId === id);
        if (item) {
            item.count += 1;
        }
    }

    decrementCount = (id: string) => {
        const item = this.items.find(item => item.productId === id);
        if (item && item.count > 1) {
            item.count -= 1;
        } else if (item) {
            this.deleteBasketProduct(id);
        }
    }

    clear = () => {
        this.items = [];
    }

    setBasketItems = (items: UserBasketProduct[]) => {
        this.items = items.map(it => ({
        ...it,
        newPrice: this.num(it.newPrice, 0),
            count: (() => { 
                const n = this.num(it.count, 1);
                return n > 0 ? Math.floor(n) : 1;
            })(),
        }));
    };
}

export default new BasketStore();