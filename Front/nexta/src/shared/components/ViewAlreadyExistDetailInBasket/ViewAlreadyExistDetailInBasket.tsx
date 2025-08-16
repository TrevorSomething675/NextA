import { useState } from 'react';
import { UpdateBasketDetailRequest } from '../../../features/basket/models/UpdateBasketDetail';
import authStore from '../../../stores/AuthStore/authStore';
import { Detail } from '../../entities/Detail';
import styles from './ViewAlreadyExistDetailInBasket.module.css';
import Button from '../Button/Button';
import BasketService from '../../../features/basket/services/BasketService';
import { useNotifications } from '../Notifications/Notifications';
import basket from '../../../stores/basket';
import { GetBasketDetailsFilter, GetBasketDetailsRequest } from '../../../features/basket/models/GetBasketDetails';

interface ViewAlreadyExistDetailInBasketProps {
    isOpen: boolean;
    onClose: () => void;
    detail:Detail
}

export const ViewAlreadyExistDetailInBasket = ({ isOpen, onClose, detail }: ViewAlreadyExistDetailInBasketProps) => {
    const { addNotification } = useNotifications();
    const [count, setCount] = useState(1);
    
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

    const handleUpdateDetail = async() => {
        const userId = authStore.user.id;
        const request:UpdateBasketDetailRequest = {
            userId: userId,
            detailId: detail.id,
            count: count
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
                header: 'Деталь успешно обновлена'
            });
            onClose();
        }
    }

    if (!isOpen) return null;
    
    return (
        <div className={styles.modal}>
            <div className={styles.modalContent}>
                <button className={styles.closeButton} onClick={onClose}>×</button>
                <h2>Деталь уже в корзине</h2>
                <div>Вы можете отредактировать количество.</div>
                <div className={styles.detailContainer}>
                    <h2 className={styles.h2}>Товар {detail.article}</h2>
                    <p className={styles.p}>
                        - {detail.description}
                    </p>
                    <div className={styles.count}>
                        Кол-во: <button type="button" className={styles.down} onClick={decrement}>◄</button>
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
                    </div>
                    <div className={styles.containerFooter}>
                        <div>
                            <span className={styles.newPrice}>
                                {detail.newPrice * count} руб.
                            </span>
                            {(detail.oldPrice !== undefined && detail.oldPrice != 0) &&
                                <span className={styles.oldPrice}>
                                    {detail.oldPrice * count} руб.
                                </span>
                            }
                        </div>
                        <Button content='Обновить корзину' className={styles.toBasket} onClick={handleUpdateDetail}/>
                    </div>
                </div>
            </div>
        </div>
    );
};