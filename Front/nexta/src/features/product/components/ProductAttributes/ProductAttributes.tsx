import React from "react"
import { ProductAttribute } from "../../../../shared/entities/ProductAttribute"
import styles from './ProductAttributes.module.css';
import { ProductAttributeItem } from "./ProductAttributeItem/ProductAttributeItem";

export const ProductAttributes: React.FC<{ attributes: ProductAttribute[] }> = ({ attributes }) => {
    return (
        <div className={styles.gridContainer}>
            {attributes && attributes.length > 0 &&
                attributes.map((attribute) => (
                    <ProductAttributeItem key={attribute.key} attribute={attribute} />
                ))
            }
        </div>
    );
};