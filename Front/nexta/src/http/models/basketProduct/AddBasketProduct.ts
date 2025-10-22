import { UserBasketProduct } from "../../../models/UserBasketProduct"

export interface AddBasketProductRequest{
    productId:string,
    userId:string,
    count:number
}