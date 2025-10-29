import { useNavigate } from "react-router-dom";
import styles from './HeaderSearchItem.module.css';
import { Product } from "../../../../../entities/product/models/product";

export const HeaderSearchItem: React.FC<{ product: Product }> = ({ product }) => {
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