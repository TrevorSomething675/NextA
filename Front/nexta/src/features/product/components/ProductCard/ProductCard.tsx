import { useState } from 'react';
import Image from '../../../../shared/components/Image/Image';
import styles from './ProductCard.module.css';
import authStore from '../../../../stores/AuthStore/authStore';
import BasketService from '../../../../services/BasketService';
import basket from '../../../../stores/basket';
import { useNotifications } from '../../../../shared/components/Notifications/Notifications';
import { Product } from '../../../../models/Product';
import { ViewAlreadyExistProductInBasket } from '../../../../shared/components/ViewAlreadyExistProductInBasket/ViewAlreadyExistProductInBasket';
import { AddBasketProductRequest } from '../../../../http/models/basketProduct/AddBasketProduct';

export const ProductCard:React.FC<{product:Product}> = ({product}) => {
    const [count, setCount] = useState(1);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const { addNotification } = useNotifications();

    const goToProductPage = () => {
        if(!isModalOpen){
            let url: string;

            if(authStore.isAdmin == true){
                url = `/Admin/Product/${product.id}`;
            } else {
                url = `/Product/${product.id}`;
            }

            window.open(url, '_blank');
        }
    };
    
    const increment = () => {
        if(count < 10){
            setCount(count => count + 1);
        }
    };

    const decrement = () => {
        if(count > 0){
            setCount(count => Math.max(1, count - 1));
        }
    };

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const value = parseInt(e.target.value, 10);
        
        if (!isNaN(value) && value >= 1 && value <= 10) {
            setCount(value);
        } else {
            setCount(1);
        }
    };
    
    const handleProductCountChange = (newCount: number) => {
        setCount(newCount);
    };

    const fetchData = async() =>{
        const request:AddBasketProductRequest = {
            userId: authStore?.user?.id ?? '',
            productId: product.id,
            countToPay: count
        };
        const response = await BasketService.AddBasketProduct(request);
        if(response.success && response.status === 200)
        {
            addNotification({
                header: `Товар ${response.data.basketProduct.name} добавлен в корзину`
            });
            basket.addBasketProduct(response.data.basketProduct)
        } else if (!response.success && response.status === 409){
            setIsModalOpen(true);
        }
    }

    return <div className={styles.container}>
        <div>
            <ViewAlreadyExistProductInBasket
                isOpen={isModalOpen}
                onClose={() => setIsModalOpen(false)}
                product={product}
                productCount={count}
                onCountChange={handleProductCountChange}
            />
        </div>
        {isModalOpen && <div className={styles.overlay} />}
        <div className={styles.productHeader} onClick={goToProductPage}>
            <Image srcImage='/defaultImage2.jpg' isBase64Image={product.image !== undefined} base64String={product?.image?.base64String} className={styles.image} />
        </div>
        <div className={styles.productBody} onClick={goToProductPage}>
            <div>
                <h2 className={styles.h2}>
                    {product.name}
                </h2>
            </div>
            <div>
                <h2 className={styles.h2}>
                    Артикул: {product.article}
                </h2>
            </div>
            <div className={styles.description}>
                {product.description}
            </div>
        </div>
        <div className={styles.productFooter}>
            <div className={styles.leftFooter}>
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
                <span className={styles.priceContainer}>
                    <span className={product.oldPrice ? styles.newPrice : styles.defaultPrice}>
                        {product.newPrice * count} руб.
                    </span>
                    {(product.oldPrice !== undefined && product.oldPrice != 0) &&
                        <span className={styles.oldPrice}>
                            {product.oldPrice * count} руб.
                        </span>
                    }
                </span>
            </div>
            <div>
                <button className={styles.addBasketBtn} onClick={fetchData}>
                    В корзину
                </button>
            </div>
        </div>
    </div>
} 