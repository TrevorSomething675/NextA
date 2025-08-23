import { useState } from 'react';
import Image from '../../../../shared/components/Image/Image';
import { Detail } from '../../../../shared/entities/Detail';
import styles from './ProductCard.module.css';
import { useNavigate } from 'react-router-dom';
import { AddBasketDetailRequest } from '../../../basket/models/AddBasketDetail';
import authStore from '../../../../stores/AuthStore/authStore';
import BasketService from '../../../basket/services/BasketService';
import { GetBasketDetailsFilter, GetBasketDetailsRequest } from '../../../basket/models/GetBasketDetails';
import basket from '../../../../stores/basket';
import { ViewAlreadyExistDetailInBasket } from '../../../../shared/components/ViewAlreadyExistDetailInBasket/ViewAlreadyExistDetailInBasket';
import { useNotifications } from '../../../../shared/components/Notifications/Notifications';

export const ProductCard:React.FC<{detail:Detail}> = ({detail}) => {
    const [count, setCount] = useState(1);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const navigate = useNavigate();
    const { addNotification } = useNotifications();

    const goToDetailPage = () => {
        if(!isModalOpen){
            navigate(`/Detail/${detail.id}`);
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
            detailId: detail.id,
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
            <ViewAlreadyExistDetailInBasket
                isOpen={isModalOpen}
                onClose={() => setIsModalOpen(false)}
                detail={detail}
                detailCount={count}
                onCountChange={handleDetailCountChange}
            />
        </div>
        {isModalOpen && <div className={styles.overlay} />}
        <div className={styles.productHeader} onClick={goToDetailPage}>
            <Image srcImage='/defaultImage2.jpg' className={styles.image} />
        </div>
        <div className={styles.productBody} onClick={goToDetailPage}>
            <div>
                <h2 className={styles.h2}>
                    {detail.name}
                </h2>
            </div>
            <div>
                <h2 className={styles.h2}>
                    Артикул: {detail.article}
                </h2>
            </div>
            <div className={styles.description}>
                {detail.description}
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
                    <span className={detail.oldPrice ? styles.newPrice : styles.defaultPrice}>
                        {detail.newPrice * count} руб.
                    </span>
                    {(detail.oldPrice !== undefined && detail.oldPrice != 0) &&
                        <span className={styles.oldPrice}>
                            {detail.oldPrice * count} руб.
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