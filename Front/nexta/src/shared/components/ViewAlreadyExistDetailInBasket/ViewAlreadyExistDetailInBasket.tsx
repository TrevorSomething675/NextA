import { useEffect } from 'react';
import { UpdateBasketDetailRequest } from '../../../features/basket/models/UpdateBasketDetail';
import authStore from '../../../stores/AuthStore/authStore';
import { Detail, DetailStatus } from '../../entities/Detail';
import styles from './ViewAlreadyExistDetailInBasket.module.css';
import Button from '../Button/Button';
import BasketService from '../../../features/basket/services/BasketService';
import { useNotifications } from '../Notifications/Notifications';
import basket from '../../../stores/basket';
import { GetBasketDetailsFilter, GetBasketDetailsRequest } from '../../../features/basket/models/GetBasketDetails';
import Image from '../Image/Image';

interface ViewAlreadyExistDetailInBasketProps {
    isOpen: boolean;
    onClose: () => void;
    detail: Detail;
    detailCount: number;
    onCountChange: (count: number) => void;
}

export const ViewAlreadyExistDetailInBasket: React.FC<ViewAlreadyExistDetailInBasketProps> = ({ 
        isOpen, 
        onClose, 
        detail, 
        detailCount,
        onCountChange
    }) => {
    const { addNotification } = useNotifications();
    
    useEffect(() => {
        onCountChange(detailCount ?? 1);
    }, [detailCount]);

    const statusLabels = {
        [DetailStatus.Unknown]: 'Неизвестный статус',
        [DetailStatus.InStock]: 'Есть на складе',
        [DetailStatus.OutOfStock]: 'Нет на складе',
    };

    const increment = () => {
        onCountChange(detailCount + 1);
    };

    const decrement = () => {
        onCountChange(Math.max(1, detailCount - 1));
    };

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const value = parseInt(e.target.value, 10);
        onCountChange(!isNaN(value) && value >= 1 ? value : 1);
    };

    const handleUpdateDetail = async() => {
        const userId = authStore.user.id;
        const request:UpdateBasketDetailRequest = {
            userId: userId,
            detailId: detail.id,
            count: detailCount
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
                        <Image isBase64Image={true} base64String={detail?.image?.base64String} className={styles.image} />
                    </div>
                    <div className={styles.detailData}>
                        <ul className={styles.ul}>
                            <li> - {detail.name}</li>
                            <li> - {detail.description}</li>
                            <li> - {statusLabels[detail.status]}</li>
                            <li> - Осталось на складе: {detail.count}</li>
                        </ul>
                        <div className={styles.detailFooter}>
                            <div>
                                <button type="button" className={styles.down} onClick={decrement}>◄</button>
                                <input
                                    value={detailCount}
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
                                    {detail.newPrice * detailCount} руб.
                                </span>
                                {(detail.oldPrice !== undefined && detail.oldPrice != 0) &&
                                    <span className={styles.oldPrice}>
                                        {detail.oldPrice * detailCount} руб.
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