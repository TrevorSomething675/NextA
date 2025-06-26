import { Detail } from "../../../shared/entities/Detail"

export interface GetDetailRequest{
    id:string
}

export interface GetDetailResponse{
    detail:Detail
}