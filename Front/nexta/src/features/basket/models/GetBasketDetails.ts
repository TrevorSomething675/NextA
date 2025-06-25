import Detail from "../../../models/Detail"

export interface GetBasketDetailsRequest{
    filter: GetBasketDetailsFilter
}

export interface GetBasketDetailsFilter {
    pageNumber:number,
    userId?:string
}

export interface GetBasketDetailsResponse {
    details:Detail[]
}