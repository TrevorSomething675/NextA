import { Detail } from "../../../shared/entities/Detail"

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