import { useEffect } from 'react';
import authStore from '../../../stores/AuthStore/authStore';
import styles from './ViewAlreadyExistProductInBasket.module.css';
import Button from '../Button/Button';
import BasketService from '../../../services/BasketService';
import { useNotifications } from '../Notifications/Notifications';
import basket from '../../../stores/basket';
import Image from '../Image/Image';
import { Product, ProductStatus } from '../../../models/Product';
import { UpdateBasketProductRequest } from '../../../http/models/basketProduct/UpdateBasketProduct';

interface ViewAlreadyExistProductInBasketProps {
    isOpen: boolean;
    onClose: () => void;
    product: Product;
    productCount: number;
    onCountChange: (count: number) => void;
}

export const ViewAlreadyExistProductInBasket: React.FC<ViewAlreadyExistProductInBasketProps> = ({ 
        isOpen, 
        onClose, 
        product, 
        productCount,
        onCountChange
    }) => {
    const { addNotification } = useNotifications();
    
    useEffect(() => {
        onCountChange(productCount ?? 1);
    }, [productCount]);

    const statusLabels = {
        [ProductStatus.Unknown]: 'Неизвестный статус',
        [ProductStatus.InStock]: 'Есть на складе',
        [ProductStatus.OutOfStock]: 'Нет на складе',
    };

    const increment = () => {
        onCountChange(productCount + 1);
    };

    const decrement = () => {
        onCountChange(Math.max(1, productCount - 1));
    };

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const value = parseInt(e.target.value, 10);
        onCountChange(!isNaN(value) && value >= 1 ? value : 1);
    };

    const handleUpdateProduct = async() => {
        const userId = authStore.user.id ?? '';

        const request:UpdateBasketProductRequest = {
            userId: userId,
            productId: product.id,
            count: productCount
        }
        const response = await BasketService.UpdateBasketProduct(request);

        if (response.success && response.status === 200) {
            basket.changeProductCount(response.data.productId, response.data.count);
            addNotification({
                header: 'Корзина обновлена'
            });
            onClose();
        }
    }

    if (!isOpen) return null;
    
    return (
        <div className={styles.modal}>
            <div className={styles.modalContent}>
                <div className={styles.header}>
                    <button className={styles.closeButton} onClick={onClose}>×</button>
                    <h2>Товар уже в корзине</h2>
                    <div>Вы можете отредактировать количество.</div>
                </div>
                <div className={styles.productContainer}>
                    <div className={styles.imageContainer}>
                        <Image isBase64Image={true} base64String={product?.image?.base64String} className={styles.image} />
                    </div>
                    <div className={styles.productData}>
                        <ul className={styles.ul}>
                            <li> - {product.name}</li>
                            <li> - {product.description}</li>
                            <li> - {statusLabels[product.status]}</li>
                            <li> - Осталось на складе: {product.count}</li>
                        </ul>
                        <div className={styles.productFooter}>
                            <div>
                                <button type="button" className={styles.down} onClick={decrement}>◄</button>
                                <input
                                    value={productCount}
                                    type="number"
                                    name="quantity"
                                    min="1"
                                    max="10"
                                    step="1"
                                    className={styles.countInput}
                                    onChange={handleInputChange}
                                />
                                <button type="button" className={styles.up} onClick={increment}>►</button>
                                <span className={styles.newPrice}>
                                    {product.newPrice * productCount} руб.
                                </span>
                                {(product.oldPrice !== undefined && product.oldPrice != 0) &&
                                    <span className={styles.oldPrice}>
                                        {product.oldPrice * productCount} руб.
                                    </span>
                                }
                            </div>
                            <div>
                                <Button className={styles.updateButton} onClick={handleUpdateProduct} content='Обновить товар' />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};