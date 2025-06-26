import styles from './BasketBody.module.css';
import { useEffect } from 'react';
import { GetBasketDetailsFilter, GetBasketDetailsRequest } from '../../models/GetBasketDetails';
import { observer } from 'mobx-react';
import basket from '../../../../stores/basket';
import { toJS } from 'mobx';
import BasketItem from '../BasketItem/BasketItem';
import BasketService from '../../services/BasketService';
import authStore from '../../../../stores/AuthStore/authStore';

const BasketBody = observer(() => {
    
    useEffect(() => {
        if(authStore.user.id !== undefined){
            const fetchData = async() => {
                const filter:GetBasketDetailsFilter = {
                pageNumber: 1,
                userId: authStore?.user?.id ?? ''
            };
            const request:GetBasketDetailsRequest = {
                filter: filter
            };
            try{
                const response = await BasketService.GetBasketDetails(request);
                basket.setBasketDetails(response.details);
            } catch(error){
                console.error(error);
            }
        }
        
        fetchData();
    }
    }, []);
    
    return <div className={styles.container}>
        {(basket?.details !== undefined) && (basket.details.length > 0) ? (<table className={styles.table}>
            <thead className={styles.thead}>
                <tr className={styles.tr}>
                    <th>Название</th>
                    <th>Артикул</th>
                    <th>Статус</th>
                    <th>Доставка в ПВЗ</th>
                    <th>Кол-во, шт</th>
                    <th>Стоимость, ₽</th>
                    <th></th>
                </tr>
            </thead>
            <tbody className={styles.tbody}>
                {toJS(basket.details).length > 0 && toJS(basket.details).map((detail) => 
                    <BasketItem detail={detail} key={detail.id} />
                )}
            </tbody>
        </table>) : (<h2 className={styles.h2}>
            Ваша корзина пуста
        </h2>)}
    </div> 
});

export default BasketBody;