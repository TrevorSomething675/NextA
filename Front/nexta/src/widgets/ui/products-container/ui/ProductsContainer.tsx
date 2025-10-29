import { Product } from "../../../../entities/product/models/product"
import { ProductCard } from "../../product-card/ui/ProductCard"
import React from "react"
import styles from './ProductsContainer.module.css';

export const ProductsContainer:React.FC<{products:Product[]}> = ({products}) => {
    return <div className={styles.container}>
        {products?.length > 0 && products.map((product) =>
            <ProductCard key={product.id} product={product}/>
        )}
    </div>
}