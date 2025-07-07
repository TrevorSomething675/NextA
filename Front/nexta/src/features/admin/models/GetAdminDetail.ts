import { AdminDetail } from "./AdminDetail"

export interface GetAdminDetailRequest{
    detailId:string,
    withImage:boolean
}

export interface GetAdminDetailResponse{
    detail:AdminDetail
}