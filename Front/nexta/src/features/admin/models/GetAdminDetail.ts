import { AdminDetail } from "./AdminDetail"

export interface GetAdminDetailRequest{
    detailId:string
}

export interface GetAdminDetailResponse{
    detail:AdminDetail
}