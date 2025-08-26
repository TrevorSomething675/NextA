import { useState } from 'react';
import Image from '../../../../shared/components/Image/Image';
import styles from './ProductCard.module.css';
import { useNavigate } from 'react-router-dom';
import { AddBasketDetailRequest } from '../../../basket/models/AddBasketDetail';
import authStore from '../../../../stores/AuthStore/authStore';
import BasketService from '../../../basket/services/BasketService';
import { GetBasketDetailsFilter, GetBasketDetailsRequest } from '../../../basket/models/GetBasketDetails';
import basket from '../../../../stores/basket';
import { useNotifications } from '../../../../shared/components/Notifications/Notifications';
import { Product } from '../../../../models/product/Product';
import { ViewAlreadyExistProductInBasket } from '../../../../shared/components/ViewAlreadyExistProductInBasket/ViewAlreadyExistProductInBasket';

export const ProductCard:React.FC<{product:Product}> = ({product}) => {
    const [count, setCount] = useState(1);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const navigate = useNavigate();
    const { addNotification } = useNotifications();

    const goToDetailPage = () => {
        if(!isModalOpen){
            navigate(`/Product/${product.id}`);
        }
    };
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
    
    const handleDetailCountChange = (newCount: number) => {
        setCount(newCount);
    };

    const fetchData = async() =>{
        const request:AddBasketDetailRequest = {
            userId: authStore?.user?.id ?? '',
            detailId: product.id,
            countToPay: count
        };
        const result = await BasketService.AddBasketDetail(request);
        if(result && result.status == 200)
        {
            addNotification({
                header: 'Товар добавлен в корзину'
            });
            const filter:GetBasketDetailsFilter = {
                pageNumber: 1,
                userId: authStore?.user?.id ?? ''
            };
            const getBasketDetailsRequest:GetBasketDetailsRequest = {
                filter: filter
            };
            const response = await BasketService.GetBasketDetails(getBasketDetailsRequest);
            basket.setBasketDetails(response.details);
        } else if (!result.success && result.status === 409){
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
                onCountChange={handleDetailCountChange}
            />
        </div>
        {isModalOpen && <div className={styles.overlay} />}
        <div className={styles.productHeader} onClick={goToDetailPage}>
            <div className={styles.offersContainer}>
                {(product.oldPrice !== 0) &&
                    <div className={styles.saleOffer}>
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" viewBox="0 0 16 16">
                            <path d="M8 16c3.314 0 6-2 6-5.5 0-1.5-.5-4-2.5-6 .25 1.5-1.25 2-1.25 2C11 4 9 .5 6 0c.357 2 .5 4-2 6-1.25 1-2 2.729-2 4.5C2 14 4.686 16 8 16m0-1c-1.657 0-3-1-3-2.75 0-.75.25-2 1.25-3C6.125 10 7 10.5 7 10.5c-.375-1.25.5-3.25 2-3.5-.179 1-.25 2 1 3 .625.5 1 1.364 1 2.25C11 14 9.657 15 8 15"/>
                        </svg>
                        Скидка
                    </div>}
                {(product?.count > 0) && 
                    <div className={styles.warehouseOffer}>
                        На складе: {product.count}
                    </div>}            
            </div>
            <Image srcImage='/defaultImage2.jpg' className={styles.image} />
        </div>
        <div className={styles.productBody} onClick={goToDetailPage}>
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