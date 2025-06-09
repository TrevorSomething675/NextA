import { useState } from 'react';
import Detail, { DetailStatus } from '../../models/Detail';
import styles from './detailItem.module.css';
import AddBasketDetailRequest from '../../models/basket/AddBasketDetailRequest';
import auth from '../../stores/auth';
import BasketService from '../../services/BasketService';
import basket from '../../stores/basket';
import BasketDetailsFilter from '../../models/basket/BasketDetailsFilter';
import GetBasketDetailsRequest from '../../models/basket/GetBasketDetailsRequest';
import { useNavigate } from 'react-router-dom';

const DetailItem:React.FC<{detail:Detail}> = ({detail}) =>{
    const [count, setCount] = useState(1);
    const navigate = useNavigate();
    const statusLabels = {
        [DetailStatus.Unknown]: 'Неизвестный статус',
        [DetailStatus.InStock]: 'Есть на складе',
        [DetailStatus.OutOfStock]: 'Нет на складе',
    };
        const goToDetailPage = () => {
        navigate(`/Detail/${detail.id}`);
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
            case DetailStatus.InStock:
            return '#1b8700';
            case DetailStatus.OutOfStock:
            return '#ed7e00';
            case DetailStatus.Unknown:
            default:
            return 'gray';
        }
    }

    const fetchData = async() =>{
        const request:AddBasketDetailRequest = {
            userId: auth?.user?.id,
            detailId: detail.id,
            countToPay: count
        };
        const result = await BasketService.AddBasketDetail(request);
        if(result.statusCode == 200 && result.value){
            const filter:BasketDetailsFilter = {
                pageNumber: 1,
                userId: auth?.user?.id
            };
            const getBasketDetailsRequest:GetBasketDetailsRequest = {
                filter: filter
            };
            const result = await BasketService.GetBasketDetails(getBasketDetailsRequest);
            if(result.statusCode == 200 && result.value){
                basket.setBasketDetails(result.value.details);
            } else {
                console.error('Ошибка на странице Home');
            };
        }
        console.log(basket.totalPrice);
    }

    const increment = () => {
        setCount(count => count + 1);
    };

    const decrement = () => {
        setCount(count => Math.max(1, count - 1));
    };

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
                    В корзину
                </button>
            </td>
        </tr>
}

export default DetailItem;