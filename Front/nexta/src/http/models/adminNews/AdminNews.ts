import { Image } from "../../../shared/entities/Image";

export interface AdminNewsResponse{
    header?:string,
    description?:string,
    image?:Image
}