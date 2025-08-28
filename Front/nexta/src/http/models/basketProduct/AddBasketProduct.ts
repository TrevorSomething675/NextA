import { BasketProduct } from "../../../models/BasketProduct"

export interface AddBasketProductRequest{
    productId:string,
    userId:string,
    countToPay:number
}

export interface AddBasketProductResponse{
    basketProduct: BasketProduct
}