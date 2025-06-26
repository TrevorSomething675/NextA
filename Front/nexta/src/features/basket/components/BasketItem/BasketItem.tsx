import { useState } from 'react';
import styles from './BasketItem.module.css';
import { observer } from 'mobx-react';
import { useNavigate } from 'react-router-dom';
import { DeleteBasketDetail } from '../../models/DeleteBasketDetail';
import auth from '../../../../stores/auth';
import { GetBasketDetailsFilter, GetBasketDetailsRequest } from '../../models/GetBasketDetails';
import basket from '../../../../stores/basket';
import { UserDetailStatus } from '../../../../shared/entities/UserDetails';
import { Detail } from '../../../../shared/entities/Detail';
import BasketService from '../../services/BasketService';

const BasketItem:React.FC<{detail:Detail}> = observer(({detail}) => {
    const [count, setCount] = useState(detail?.userDetails[0]?.count ?? 0);
    const navigate = useNavigate();
    const statusLabels = {
        [UserDetailStatus.Unknown]: 'Неизвестный статус',
        [UserDetailStatus.Accepted]: 'Принят',
        [UserDetailStatus.AtWork]: 'В работе',
        [UserDetailStatus.Rejected]: 'Отказ',
        [UserDetailStatus.Waiting]: 'Ожидает'
    };

    const goToDetailPage = () => {
        navigate(`/Detail/${detail.id}`);
    };

    const fetchData = async() =>{
        const request:DeleteBasketDetail = {
            userId: auth?.user?.id,
            detailId: detail.id
        };
        await BasketService.DeletebasketDetail(request);

        const filter:GetBasketDetailsFilter = {
            pageNumber: 1,
            userId: auth?.user?.id
        };
        const getBasketDetailsRequest:GetBasketDetailsRequest = {
            filter: filter
        };
        const result = await BasketService.GetBasketDetails(getBasketDetailsRequest);
        basket.setBasketDetails(result.details);
    };

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const value = parseInt(e.target.value, 10);
        
        if (!isNaN(value) && value >= 1) {
            setCount(value);
        } else {
            setCount(1);
        }
    };

    const increment = () => {
        setCount(count => count + 1);
    };

    const decrement = () => {
        setCount(count => Math.max(1, count - 1));
    };
    
    return <tr className={styles.tr}>
        <td>
            <button onClick={goToDetailPage} className={styles.button}>{detail.name}</button>
        </td>
        <td>
            {detail.article}
        </td>
        <td>{statusLabels[detail?.userDetails[0]?.status]}</td>
        <td>{detail.userDetails !== undefined && detail?.userDetails[0]?.deliveryDate}</td>
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
            <span className={styles.newPrice}>
                {detail.newPrice} руб.
            </span>
            {(detail.oldPrice !== undefined && detail.oldPrice != 0) &&
                <span className={styles.oldPrice}>
                    {detail.oldPrice} руб.
                </span>
            }
        </td>
        <td className={styles.trashContainer}>
        <button className={styles.removeBasketBtn} onClick={fetchData}>
            <svg xmlns="http://www.w3.org/2000/svg" className={styles.trash} fill="currentColor" viewBox="0 0 16 16">
                <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0z"/>
                <path d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4zM2.5 3h11V2h-11z"/>
            </svg>
        </button>
        </td>
    </tr>
});

export default BasketItem;