import { ProductStatus } from '../../../../../models/Product';
import { useState } from "react";
import styles from './AddProductToOrderItem.module.css';
import { AdminProduct } from '../../../models/AdminProduct';

interface AddProductToOrderItemProps {
    product: AdminProduct;
    onAddToOrder: (product: AdminProduct, count: number) => void;
}

export const AddProductToOrderItem: React.FC<AddProductToOrderItemProps> = ({ product, onAddToOrder }) => {
    const [count, setCount] = useState(1);
    const statusLabels = {
        [ProductStatus.Unknown]: 'Неизвестный статус',
        [ProductStatus.InStock]: 'Есть на складе',
        [ProductStatus.OutOfStock]: 'Нет на складе',
    };
        const goToProductPage = () => {
        window.open(`/Admin/Product/${product.id}`);
    }

    const getColorForStatus = (status:any) => {
        switch (status) {
            case ProductStatus.InStock:
            return '#1b8700';
            case ProductStatus.OutOfStock:
            return '#ed7e00';
            case ProductStatus.Unknown:
            default:
            return 'gray';
        }
    }

    const increment = () => {
        setCount(count => count + 1);
    };

    const decrement = () => {
        setCount(count => Math.max(1, count - 1));
    };

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const value = parseInt(e.target.value, 10);
        
        if (!isNaN(value) && value >= 1) {
            setCount(value);
        } else {
            setCount(1);
        }
    };

    const fetchData = async() =>{
        const productToAdd = product as AdminProduct;
        const countToAdd = count;

        onAddToOrder(productToAdd, countToAdd);
    }

    return <tr className={styles.tr}>
            <td>
                <button onClick={goToProductPage} className={styles.button}>
                    {product.name}
                </button>
            </td>
            <td>{product.article}</td>
            <td>{product.description}</td>
            <td style={{color: getColorForStatus(product.status)}}>{statusLabels[product.status]}</td>
            <td>{product.deliveryDate}</td>
            <td>
                <button type="button" className={styles.down} onClick={decrement}>◄</button>
                    <input
                        value={count}
                        type="number"
                        name="quantity"
                        min="1"
                        max="10"
                        step="1"
                        className={styles.countInput}
                        onChange={handleInputChange}
                    />
                <button type="button" className={styles.up} onClick={increment}>►</button>
            </td>
            <td>
                <span className={product.oldPrice ? styles.newPrice : styles.defaultPrice}>
                    {product.newPrice * count} руб.
                </span>
                {(product.oldPrice !== undefined && product.oldPrice != 0) &&
                    <span className={styles.oldPrice}>
                        {product.oldPrice * count} руб.
                    </span>
                }
            </td>
            <td>
                <button className={styles.addBasketBtn} onClick={fetchData}>
                    Добавить к заказу
                </button>
            </td>
        </tr>
}