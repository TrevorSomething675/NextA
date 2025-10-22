import styles from './BasketBody.module.css';
import { useEffect } from 'react';
import { observer } from 'mobx-react';
import basket from '../../../../../stores/basket';
import BasketService from '../../../../../services/BasketService';
import authStore from '../../../../../stores/AuthStore/authStore';
import BasketItem from '../BasketItem/BasketItem';

const BasketBody = observer(() => {
    useEffect(() => {
        if(authStore.user.id !== undefined){
            const fetchData = async() => {
                const userId = authStore?.user?.id ?? '';

                try{
                    const response = await BasketService.GetBasketProducts(userId);
                    if(response.success && response.status === 200){
                        basket.setBasketItems(response.data.products);
                    }
                } 
                catch(error){
                    console.error(error);
                }
            }
        
        fetchData();
    }
    }, []);
    
    return <div className={styles.container}>
        {(basket?.items !== undefined) && (basket.items.length > 0) ? (<table className={styles.table}>
            <thead className={styles.thead}>
                <tr className={styles.tr}>
                    <th>Название</th>
                    <th>Артикул</th>
                    <th>Описание</th>
                    <th>Кол-во, шт</th>
                    <th></th>
                    <th>Стоимость, ₽</th>
                    <th></th>
                </tr>
            </thead>
            <tbody className={styles.tbody}>
                {basket.items.length > 0 && basket.items.map((product) => 
                    <BasketItem product={product} key={product.productId} />
                )}
            </tbody>
        </table>)
        :
        (<div className={styles.noBasketProducts}>
            Ваша корзина пуста.
        </div>)}
    </div> 
});

export default BasketBody;