import styles from './SearchProductsContainer.module.css';
import { ProductCard } from '../ProductCard/ProductCard';
import { Product } from '../../../../models/Product';

export const SearchProductsContainer: React.FC<{ products?: Product[] }> = ({ products }) => {
    if (!products || products.length === 0) {
        return <div>Ничего не найдено</div>;
    }

    return (
        <div className={styles.container}>
            <div className={styles.products}>
                {products.map((product) => (
                    <ProductCard key={product.id} product={product} />
                ))}
            </div>
        </div>
    );
};