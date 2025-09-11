import { useNavigate } from "react-router-dom";
import { AdminProduct } from "../../../models/AdminProduct";
import styles from './AdminProductSearchItem.module.css';

export const SearchItem:React.FC<{product:AdminProduct}> = ({product}) =>{
    const navigate = useNavigate();
    const goToProductPage = () => {
        navigate(`/Product/${product.id}`);
    };

    return <div className={styles.container} onClick={goToProductPage}>
        <div>
            {product.name}
        </div>
        <div>
            {product.article}
        </div>
        <div>
            Кол-во, шт: {product.count}
        </div>
        <div>
            <span className={product.oldPrice ? styles.newPrice : styles.defaultPrice}>
                {product.newPrice} руб.
            </span>
            {(product.oldPrice !== undefined && product.oldPrice != 0) &&
                <span className={styles.oldPrice}>
                    {product.oldPrice} руб.
                </span>
            }
        </div>
    </div>
}