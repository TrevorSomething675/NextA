import { useState } from 'react';
import styles from './BasketItem.module.css';
import { observer } from 'mobx-react';
import { useNavigate } from 'react-router-dom';
import { DeleteBasketDetail } from '../../models/DeleteBasketDetail';
import { GetBasketDetailsFilter, GetBasketDetailsRequest } from '../../models/GetBasketDetails';
import basket from '../../../../stores/basket';
import { UserDetailStatus } from '../../../../shared/entities/UserDetails';
import { Detail } from '../../../../shared/entities/Detail';
import BasketService from '../../services/BasketService';
import authStore from '../../../../stores/AuthStore/authStore';
import CheckSvg from '../../../../shared/svgs/CheckSvg/CheckSvg';
import TrashSvg from '../../../../shared/svgs/TrashSvg/TrashSvg';
import { UpdateBasketDetailRequest } from '../../models/UpdateBasketDetail';
import { useNotifications } from '../../../../shared/components/Notifications/Notifications';

const BasketItem:React.FC<{detail:Detail}> = observer(({detail}) => {
    const [count, setCount] = useState(detail?.userDetails[0]?.count ?? 0);
    const [legacyCount, setLegacyCount] = useState(detail?.userDetails[0]?.count);
    const navigate = useNavigate();
    const {addNotification} = useNotifications();

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
            userId: authStore?.user?.id ?? '',
            detailId: detail.id
        };
        await BasketService.DeleteBasketDetail(request);

        const filter:GetBasketDetailsFilter = {
            pageNumber: 1,
            userId: authStore?.user?.id ?? '',
        };
        const getBasketDetailsRequest:GetBasketDetailsRequest = {
            filter: filter
        };
        const result = await BasketService.GetBasketDetails(getBasketDetailsRequest);
        basket.setBasketDetails(result.details);
    };

    const handleUpdateDetail = async(detailId:string, count:number) => {
        const request: UpdateBasketDetailRequest = {
            userId: authStore?.user?.id,
            detailId: detailId,
            count: count
        }
        const response = await BasketService.UpdateBasketDetail(request);
        if(response){
            setLegacyCount(response.count);
            addNotification({
                header: 'Корзина обновлена',
                body: `Деталь: ${detail.name}. Изменения успешно внесены.`
            })
        }
    }

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const value = parseInt(e.target.value, 10);
        
        if (!isNaN(value) && value >= 1) {
            setCount(value);
            detail.userDetails[0].count = value;
        } else {
            setCount(1);
            detail.userDetails[0].count = value;
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
            x
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
        <td className={styles.buttonsContainer}>
            {(count != legacyCount) &&
                <button className={styles.updateBasketBtn}
                data-tooltip='Подтвердить изменения' 
                onClick={() => handleUpdateDetail(detail.id, count)}>
                    <CheckSvg />
                </button>
            }
            <button className={styles.removeBasketBtn} data-tooltip='Удалить из корзины' onClick={fetchData}>
                <TrashSvg />
            </button>
        </td>
    </tr>
});

export default BasketItem;