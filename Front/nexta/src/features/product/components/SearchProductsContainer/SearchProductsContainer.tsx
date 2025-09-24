import styles from './SearchProductsContainer.module.css';
import { ProductCard } from '../ProductCard/ProductCard';
import { Product } from '../../../../models/Product';

export const SearchProductsContainer: React.FC<{ products?: Product[] }> = ({ products }) => {
    if (!products || products.length === 0) {
        return <div className={styles.notFoundContainer}>
            <div className={styles.notFound}>
                Ничего не найдено
            </div>
            <div className={styles.notFound}>
                Вы можете позвонить нам по номеру +7 (915) 562-95-13 и мы сделаем заказ для вас!
            </div>
        </div>;
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