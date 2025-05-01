'use client'

import GetBasketDetailsRequest from "@/models/basket/GetBasketDetailsRequest";
import BasketService from '@/services/BasketService';
import BasketItem from './basketItem/busketItem';
import styles from './basket.module.css';
import { useEffect } from 'react';
import basket from "@/stores/basket";

const Basket = () => {
    useEffect(() => {
        if(typeof window !== 'undefined')
        {
            const localUserId = localStorage.getItem('id');
            const getBasketDetailsRequest:GetBasketDetailsRequest ={
                filter:{
                    pageNumber: 1,
                    userId: localUserId
                }
            } 
            BasketService.GetBasketDetails(getBasketDetailsRequest)
            .then(result => {
                basket.setBasketDetails(result?.data?.value);
                console.log(result.data);
            }).catch(error =>{
                console.error(error);
            });
        }
    }, []);
    
    
    return <div className={styles.container}> 
        <div className={styles.basketHeader}>
            <h2 className={styles.h2}>Ваша корзина</h2>
        </div>
        <div className={styles.basketBody}>
            <ul>
                <BasketItem />
            </ul>
        </div>
        <div className={styles.basketFooter}>
            <button className={styles.buyBasketBtn}>
                Перейти к оформлению
                <svg xmlns="http://www.w3.org/2000/svg" className={styles.arrow} fill="currentColor" viewBox="0 0 16 16">
                    <path d="M1 8a.5.5 0 0 1 .5-.5h11.793l-3.147-3.146a.5.5 0 0 1 .708-.708l4 4a.5.5 0 0 1 0 .708l-4 4a.5.5 0 0 1-.708-.708L13.293 8.5H1.5A.5.5 0 0 1 1 8"/>
                </svg>
            </button>
            <div className={styles.resultSum}>
                Итоговая сумма: 1000.66 руб.
            </div>
        </div>
    </div>
}

export default Basket;