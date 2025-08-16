import { DetailStatus } from "../../../shared/entities/Detail";

export interface CreateAdminDetailRequest {
    name:string,
    article:string,
    description:string,
    status:DetailStatus,
    count:number,
    newPrice:number,
    oldPrice?:number | null,
    isVisible:boolean,
    image?: CreateAdminDetailImageRequest
}

export interface CreateAdminDetailResponse {
    id:string;
}

export interface CreateAdminDetailImageRequest{
    name:string,
    base64string:string
}