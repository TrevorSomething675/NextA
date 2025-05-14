import { useState } from 'react';
import Detail, { DetailStatus } from '../../models/Detail';
import styles from './detailItem.module.css';
import AddBasketDetailRequest from '../../models/basket/AddBasketDetailRequest';
import auth from '../../stores/auth';
import BasketService from '../../services/BasketService';
import basket from '../../stores/basket';
import BasketDetailsFilter from '../../models/basket/BasketDetailsFilter';
import GetBasketDetailsRequest from '../../models/basket/GetBasketDetailsRequest';

const DetailItem:React.FC<{detail:Detail}> = ({detail}) =>{
    const [count, setCount] = useState(1);
    const statusLabels = {
        [DetailStatus.Unkown]: 'Неизвестный статус',
        [DetailStatus.InStock]: 'Есть на складе',
        [DetailStatus.OutOfStock]: 'Нет на складе',
    };

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
      setCount(count => count - 1);
    };

    return <tr className={styles.tr}>
            <td>{detail.name}</td>
            <td>{detail.article}</td>
            <td>{detail.description}</td>
            <td>{statusLabels[detail.status]}</td>
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
                        onChange={(e) => setCount(Number(e.target.value))}
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
            <td>
            <button className={styles.addBasketBtn} onClick={fetchData}>
                В корзину
            </button>
            </td>
        </tr>
}

export default DetailItem;