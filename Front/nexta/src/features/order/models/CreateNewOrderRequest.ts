export interface CreateNewOrderRequest{
    userId:string,
    detailIds:string[]
}

export interface CreateNewOrderResponse{
    id:string;
}