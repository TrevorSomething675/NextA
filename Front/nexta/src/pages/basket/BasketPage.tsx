import BasketItem from '../../components/basketItem/busketItem';
import styles from './basket.module.css';
import { useEffect } from 'react';
import BasketService from '../../services/BasketService';
import GetBasketDetailsRequest from '../../models/basket/GetBasketDetailsRequest';
import BasketDetailsFilter from '../../models/basket/BasketDetailsFilter';
import auth from '../../stores/auth';
import basket from '../../stores/basket';

const BasketPage = () => {
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
            if(result.statusCode == 200){
                basket.setBasketDetails(result.value);
            } else {
                console.error('Ошибка на странице BasketPage');
            };
        }
        fetchData();
        }, []);
    
    return <div className={styles.container}> 
        <div className={styles.basketHeader}>
            <h2 className={styles.h2}>Ваша корзина</h2>
        </div>
        <div className={styles.basketBody}>
        {basket?.details?.length > 0 ?(
            <ul>
                {basket.details.map((detail) => <BasketItem detail={detail}/>)}
            </ul>
        ) : (<p className={styles.text}>Корзина пуста</p>)}
        </div>
        <div className={styles.basketFooter}>
            <button className={styles.buyBasketBtn}>
                Перейти к оформлению
                <svg xmlns="http://www.w3.org/2000/svg" className={styles.arrow} fill="currentColor" viewBox="0 0 16 16">
                    <path d="M1 8a.5.5 0 0 1 .5-.5h11.793l-3.147-3.146a.5.5 0 0 1 .708-.708l4 4a.5.5 0 0 1 0 .708l-4 4a.5.5 0 0 1-.708-.708L13.293 8.5H1.5A.5.5 0 0 1 1 8"/>
                </svg>
            </button>
            <div className={styles.resultSum}>
                Итоговая сумма: {basket.totalPrice}
            </div>
        </div>
    </div>
}

export default BasketPage;