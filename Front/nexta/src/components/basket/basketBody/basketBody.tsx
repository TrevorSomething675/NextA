import { useEffect } from 'react';
import styles from './basketBody.module.css';
import BasketDetailsFilter from '../../../models/basket/BasketDetailsFilter';
import GetBasketDetailsRequest from '../../../models/basket/GetBasketDetailsRequest';
import BasketService from '../../../services/BasketService';
import auth from '../../../stores/auth';
import basket from '../../../stores/basket';
import { toJS } from 'mobx';
import { observer } from 'mobx-react';
import BasketItem from '../basketItem/basketItem';

const BasketBody = observer(() => {
    useEffect(() => {
        const fetchData = async() => {
            const filter:BasketDetailsFilter = {
                pageNumber: 1,
                userId: auth?.user?.id
            };
            const request:GetBasketDetailsRequest = {
                filter: filter
            };
            const result = await BasketService.GetBasketDetails(request);
            if(result.statusCode == 200 && result.value){
                basket.setBasketDetails(result.value.details);
            } else {
                console.error('Ошибка на странице BasketBody');
            };
        }
        
        fetchData();
    }, []);
    return <div className={styles.container}>
        <table className={styles.table}>
            <thead className={styles.thead}>
                <tr className={styles.tr}>
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
        </table>
    </div> 
});

export default BasketBody;