export interface CreateNewOrderRequest{
    userId:string,
    productIds:string[]
}

export interface CreateNewOrderResponse{
    id:string
}