import { Image } from "./Image"

export interface NewsResponse{
    news: News[]
}

export interface News{
    header?:string,
    description?:string,
    image?:Image
}