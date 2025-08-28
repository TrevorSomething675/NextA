import { useState } from "react";
import { useNavigate } from "react-router-dom";
import styles from './AdminDetailItem.module.css';
import { Product, ProductStatus } from "../../../../../models/Product";

export const AdminProductItem:React.FC<{product:Product}> = ({product}) =>{

    const [count, setCount] = useState(1);
    const navigate = useNavigate();
    const statusLabels = {
        [ProductStatus.Unknown]: 'Неизвестный статус',
        [ProductStatus.InStock]: 'Есть на складе',
        [ProductStatus.OutOfStock]: 'Нет на складе',
    };
        const goToProductPage = () => {
        navigate(`/Admin/Product/${product.id}`);
    }

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const value = parseInt(e.target.value, 10);
        
        if (!isNaN(value) && value >= 1) {
            setCount(value);
        } else {
            setCount(1);
        }
    };

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

    const fetchData = async() =>{
        /*
        const request:AddBasketDetailRequest = {
            userId: authStore?.user?.id ?? '',
            detailId: detail.id,
            countToPay: count
        };
        const response = await BasketService.AddBasketDetail(request);
        if(response)
        {
            const filter:GetBasketDetailsFilter = {
                pageNumber: 1,
                userId: authStore?.user?.id ?? ''
            };
            const getBasketDetailsRequest:GetBasketDetailsRequest = {
                filter: filter
            };
            const result = await BasketService.GetBasketDetails(getBasketDetailsRequest);
            basket.setBasketDetails(result.details);
        }
        */
    }

    const increment = () => {
        setCount(count => count + 1);
    };

    const decrement = () => {
        setCount(count => Math.max(1, count - 1));
    };

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
                <button className={styles.addToBasketBtn} onClick={fetchData}>
                    В корзину
                </button>
            </td>
        </tr>
}