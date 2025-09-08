import { UserBasketProduct } from "../../../models/UserBasketProduct"

export interface AddBasketProductRequest{
    productId:string,
    userId:string,
    countToPay:number
}

export interface AddBasketProductResponse{
    basketProduct: UserBasketProduct
}