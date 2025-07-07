import { DetailStatus } from "../../../shared/entities/Detail";

export interface UpdateAdminDetailRequest{
    id:string,
    name:string,
    article:string,
    description:string,
    status:DetailStatus,
    count:number,
    newPrice:number,
    oldPrice?:number,
    imageId:string,
    imageName:string,
    imageBase64string:string,
    isVisible:boolean
}