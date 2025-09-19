import { ProductAttribute } from '../../../../../shared/entities/ProductAttribute';
import styles from './ProductAttributeItem.module.css';

export const ProductAttributeItem: React.FC<{ attribute: ProductAttribute }> = ({ attribute }) => {
    return (
        <div className={styles.container}>
            <div className={styles.key}>{attribute?.key}</div>
            <div className={styles.dots}></div>
            <div className={styles.value}>{attribute?.value}</div>
        </div>
    );
};