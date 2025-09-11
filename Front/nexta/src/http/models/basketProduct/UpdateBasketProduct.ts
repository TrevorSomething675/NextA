export interface UpdateBasketProductRequest{
    userId:string,
    productId:string,
    deliveryDate?:string,
    count:number
}

export interface UpdateBasketProductResponse{
    userId:string,
    productId:string,
    deliveryDate:string,
    count:number
}