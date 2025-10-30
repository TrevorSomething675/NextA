import styles from './BasketBody.module.css';
import { useEffect } from 'react';
import { observer } from 'mobx-react';
import authStore from '../../../../../shared/stores/auth/authStore';
import { BasketApi } from '../../../../../shared/http/basket/basketApi';
import basketStore from '../../../../../shared/stores/basket/basketStore';
import { BasketProductItem } from '../../basketProductItem/ui/BasketProductItem';

export const BasketBody = observer(() => {
    useEffect(() => {
        if(authStore.user.id !== undefined){
            const fetchData = async() => {
                const userId = authStore?.user?.id ?? '';
                try{
                    const response = await BasketApi.GetByUserId(userId);
                    if(response.success && response.status === 200){
                        basketStore.setBasketItems(response.data.products);
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
        {(basketStore?.items !== undefined) && (basketStore.items.length > 0) ? (<table className={styles.table}>
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
                {basketStore.items.length > 0 && basketStore.items.map((product) => 
                    <BasketProductItem basketProduct={product} key={product.productId} />
                )}
            </tbody>
        </table>)
        :
        (<div className={styles.noBasketProducts}>
            Ваша корзина пуста.
        </div>)}
    </div> 
});