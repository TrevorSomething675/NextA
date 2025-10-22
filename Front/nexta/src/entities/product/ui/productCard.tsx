import { Product } from "../models/product"

export const ProductCard:React.FC<Product> = (product:Product) => {
    return <>
        {product}
    </>
}