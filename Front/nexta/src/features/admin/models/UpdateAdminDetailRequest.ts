import { DetailStatus } from "../../../shared/entities/Detail";

export interface UpdateAdminDetailRequest{
    id:string,
    name:string,
    article:string,
    description:string,
    status:DetailStatus,
    count?:number,
    newPrice:number,
    oldPrice?:number,
    isVisible:boolean,
    imageId?:string,
    image?: UpdateAdminDetailImageRequest
}


export interface UpdateAdminDetailImageRequest{
    name:string,
    base64string:string
}