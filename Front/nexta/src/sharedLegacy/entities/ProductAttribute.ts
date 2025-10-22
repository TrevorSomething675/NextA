import { Product } from "../../models/Product";

export interface ProductAttribute {
    productId:string,
    product?:Product,
    key:string,
    value:string
}