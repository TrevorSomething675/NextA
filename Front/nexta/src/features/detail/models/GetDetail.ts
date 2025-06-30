import { DetailStatus } from "../../../shared/entities/Detail"
import { Image } from "../../../shared/entities/Image"

export interface GetDetailRequest{
    id:string
}

export interface GetDetailResponse{
    id:string,
    name:string,
    article:string,
    description:string,
    status:DetailStatus,
    orderDate:string,
    deliveryDate:string,
    count:number,
    newPrice:number,
    oldPrice:number,
    image:Image
}