import { useState } from "react";
import { Detail, DetailStatus } from "../../../../shared/entities/Detail";
import styles from './DetailBody.module.css';
import Image from "../../../../shared/components/Image/Image";
import BasketService from "../../../basket/services/BasketService";
import { AddBasketDetailRequest } from "../../../basket/models/AddBasketDetail";
import { useNotifications } from "../../../../shared/components/Notifications/Notifications";
import basket from "../../../../stores/basket";
import { GetBasketDetailsFilter, GetBasketDetailsRequest } from "../../../basket/models/GetBasketDetails";
import authStore from "../../../../stores/AuthStore/authStore";
import { ViewAlreadyExistDetailInBasket } from "../../../../shared/components/ViewAlreadyExistDetailInBasket/ViewAlreadyExistDetailInBasket";

const statusLabels = {
    [DetailStatus.Unknown]: 'Неизвестный статус',
    [DetailStatus.InStock]: 'Есть на складе',
    [DetailStatus.OutOfStock]: 'Нет на складе',
};

const DetailBody:React.FC<{detail:Detail}> = ({detail}) => {
    const [count, setCount] = useState(1);
    const { addNotification } = useNotifications();
    const [isModalOpen, setIsModalOpen] = useState(false);

    const increment = () => {
        setCount(count => count + 1);
    };

    const decrement = () => {
        setCount(count => Math.max(1, count - 1));
    };
    
    const handleDetailCountChange = (newCount: number) => {
        setCount(newCount);
    };

    const handleAddToBasket = async () => {
        const userId = localStorage.getItem('userId') ?? '';
        const detailId = detail.id;
        const countToPay = count;

        const request: AddBasketDetailRequest = {
            userId: userId,
            detailId: detailId,
            countToPay: countToPay
        };

        const result = await BasketService.AddBasketDetail(request);
        if (result.success && result.status === 200) {
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
                header: 'Товар добавлен в корзину'
            });
        } 
        else if (!result.success && result.status === 409) {
            setIsModalOpen(true);
        }
    };

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const value = parseInt(e.target.value, 10);
        
        if (!isNaN(value) && value >= 1) {
            setCount(value);
        } else {
            setCount(1);
        }
    };

    return <div className={styles.container}>
        <div className={styles.imageContainer}>
            <Image isBase64Image={true} base64String={detail?.image?.base64String} className={styles.image} />
        </div>
        <div className={styles.detailContainer}>
            <ul className={styles.ul}>
                <li> - {detail.name}</li>
                <li> - {detail.description}</li>
                <li> - {statusLabels[detail.status]}</li>
                <li> - Осталось на складе: {detail.count}</li>
            </ul>
            <div className={styles.detailFooter}>
                <div className={styles.priceContainer}>
                    <div className={styles.countContainer}>
                        <div>
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
                        </div>
                        <span className={styles.newPrice}>
                            {detail.newPrice * count} руб.
                        </span>
                        {(detail.oldPrice !== undefined && detail.oldPrice != 0) &&
                            <span className={styles.oldPrice}>
                                {detail.oldPrice * count} руб.
                            </span>
                        }
                    </div>
                    <button className={styles.buyButton} onClick={handleAddToBasket}>
                        В корзину
                    </button>
                </div>
            </div>
        </div>
        <div className={styles.rightBar}>
            <ViewAlreadyExistDetailInBasket
                isOpen={isModalOpen}
                onClose={() => setIsModalOpen(false)}
                detail={detail}
                detailCount={count}
                onCountChange={handleDetailCountChange}
            />
        </div>
        {isModalOpen && <div className={styles.overlay} />}
    </div>
}

export default DetailBody;