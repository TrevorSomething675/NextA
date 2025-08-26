import { useEffect } from 'react';
import { UpdateBasketDetailRequest } from '../../../features/basket/models/UpdateBasketDetail';
import authStore from '../../../stores/AuthStore/authStore';
import styles from './ViewAlreadyExistProductInBasket.module.css';
import Button from '../Button/Button';
import BasketService from '../../../features/basket/services/BasketService';
import { useNotifications } from '../Notifications/Notifications';
import basket from '../../../stores/basket';
import { GetBasketDetailsFilter, GetBasketDetailsRequest } from '../../../features/basket/models/GetBasketDetails';
import Image from '../Image/Image';
import { Product, ProductStatus } from '../../../models/product/Product';

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

    const handleUpdateDetail = async() => {
        const userId = authStore.user.id;
        const request:UpdateBasketDetailRequest = {
            userId: userId,
            detailId: product.id,
            count: productCount
        }
        const response = await BasketService.UpdateBasketDetail(request);
        if (response.success && response.status === 200) {
            const filter: GetBasketDetailsFilter = {
                pageNumber: 1,
                userId: authStore?.user?.id ?? ''
            };
            const getBasketDetailsRequest: GetBasketDetailsRequest = {
                filter: filter
            };
            const basketResult = await BasketService.GetBasketDetails(getBasketDetailsRequest);
            basket.setBasketDetails(basketResult.details);
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
                <div className={styles.detailContainer}>
                    <div className={styles.imageContainer}>
                        <Image isBase64Image={true} base64String={product?.image?.base64String} className={styles.image} />
                    </div>
                    <div className={styles.detailData}>
                        <ul className={styles.ul}>
                            <li> - {product.name}</li>
                            <li> - {product.description}</li>
                            <li> - {statusLabels[product.status]}</li>
                            <li> - Осталось на складе: {product.count}</li>
                        </ul>
                        <div className={styles.detailFooter}>
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
                                <Button className={styles.updateButton} onClick={handleUpdateDetail} content='Обновить товар' />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};