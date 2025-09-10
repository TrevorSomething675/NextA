import { useNavigate } from "react-router-dom";
import { Product } from "../../../../models/Product";
import styles from './HeaderSearchItem.module.css';

export const SearchItem: React.FC<{ product: Product }> = ({ product }) => {
    const navigate = useNavigate();
    
    const goToProductPage = () => {
        navigate(`/Product/${product.id}`);
    };

    return (
        <div className={styles.container} onClick={goToProductPage}>
            <div className={styles.searchProducts}>
                <div className={styles.name}>
                    {product.name}
                </div>
                <div className={`${styles.productItemDescription}`}>
                    {product.description}
                </div>
                <div className={styles.article}>
                    {product.article}
                </div>
                <div className={styles.count}>
                    Кол-во: {product.count} шт
                </div>
                <div className={styles.price}>
                    <div className={styles.priceContainer}>
                        <span className={product.oldPrice ? styles.newPrice : styles.defaultPrice}>
                            {product.newPrice} руб.
                        </span>
                        {product.oldPrice !== 0 && (
                            <span className={styles.oldPrice}>
                                {product.oldPrice} руб.
                            </span>
                        )}
                    </div>
                </div>
                </div>
        </div>
    );
};