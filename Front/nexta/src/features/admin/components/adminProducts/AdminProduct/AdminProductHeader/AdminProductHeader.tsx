import { AdminProduct } from "../../../../models/AdminProduct"
import styles from './AdminProductHeader.module.css';

const AdminProductHeader: React.FC<{product:AdminProduct}> = ({ product }) => {
    return (
        <div className={styles.container}>
            <h2 className={styles.h2}>
                Товар {product?.article}
            </h2>
        </div>
    );
};

export default AdminProductHeader;