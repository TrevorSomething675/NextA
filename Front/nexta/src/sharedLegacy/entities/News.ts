import { Image } from "./Image"

export interface News{
    id:string,
    header?:string,
    description?:string,
    image?:Image
}