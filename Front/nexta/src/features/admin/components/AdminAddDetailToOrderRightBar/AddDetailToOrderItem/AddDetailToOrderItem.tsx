import { Product, ProductStatus } from '../../../../../models/Product';
import styles from './AddDetailToOrderItem.module.css';
import { useState } from "react";

interface AddDetailToOrderItemProps {
    detail: Product;
    onAddToOrder: (detail: Product, count: number) => void;
}

export const AddDetailToOrderItem: React.FC<AddDetailToOrderItemProps> = ({ detail, onAddToOrder }) => {
    const [count, setCount] = useState(1);
    const statusLabels = {
        [ProductStatus.Unknown]: 'Неизвестный статус',
        [ProductStatus.InStock]: 'Есть на складе',
        [ProductStatus.OutOfStock]: 'Нет на складе',
    };
        const goToDetailPage = () => {
        window.open(`/Admin/Detail/${detail.id}`);
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
        const detailToAdd = detail as Product;
        const countToAdd = count;

        onAddToOrder(detailToAdd, countToAdd);
    }

    return <tr className={styles.tr}>
            <td>
                <button onClick={goToDetailPage} className={styles.button}>
                    {detail.name}
                </button>
            </td>
            <td>{detail.article}</td>
            <td>{detail.description}</td>
            <td style={{color: getColorForStatus(detail.status)}}>{statusLabels[detail.status]}</td>
            <td>{detail.deliveryDate}</td>
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
                <span className={detail.oldPrice ? styles.newPrice : styles.defaultPrice}>
                    {detail.newPrice * count} руб.
                </span>
                {(detail.oldPrice !== undefined && detail.oldPrice != 0) &&
                    <span className={styles.oldPrice}>
                        {detail.oldPrice * count} руб.
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