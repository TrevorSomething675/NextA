import { ProductAttribute } from '../../../entities/product/productAttribute';
import styles from './ProductAttributes.module.css';

export const ProductAttributes: React.FC<{ attributes: ProductAttribute[] }> = ({ attributes }) => {
    return (
        <div className={styles.gridContainer}>
            {attributes && attributes.length > 0 &&
                attributes.map((attribute) => (
                    <div key={attribute.key}>
                        <div className={styles.key}>{attribute?.key}</div>
                        <div className={styles.dots}></div>
                        <div className={styles.value}>{attribute?.value}</div>
                    </div>
                ))
            }
        </div>
    );
};