import { observer } from 'mobx-react';
import styles from './BasketPage.module.css';
import BasketItem from '../../../featuresLegacy/basket/components/Basket/BasketItem/BasketItem';
import Button from '../../../shared/ui/button/Button';
import { useNavigate } from 'react-router-dom';
import { useNotifications } from '../../../sharedLegacy/components/Notifications/Notifications';
import { useState } from 'react';
import authStore from '../../../shared/stores/auth/authStore';
import { OrderApi } from '../../../entities/order/api/orderApi';
import { OrderItem } from '../../../entities/order/models/orderItem';
import basketStore from '../../../shared/stores/basket/basketStore';

const BasketPage = observer(() => {
    const navigate = useNavigate();
    const {addNotification} = useNotifications();

    const [isLoading, setLoading] = useState(false);

    const handleCreateOrder = () =>{
        fetchData();
    }
    
    const fetchData = async() => {
        const userId = authStore?.user?.id ?? '';
        const products = basketStore.items.map((product) => ({
            productId:product.productId,
            product:product.product
        }) as OrderItem);

        setLoading(true);

        const response = await OrderApi.CreateNewOrder(userId, products);
        
        if(response.success && response.status === 200){
            basketStore.clear();
            
            const newOrdersResponse = await OrderService.GetOrdersForUser(userId);

            if(newOrdersResponse.success && newOrdersResponse.status === 200){
                orderStore.setOrderItems(newOrdersResponse.data.data.items);
            }  
            
            navigate('/Order');
            addNotification({
                header: 'Заказ сформирован',
                body: `Ваш заказ [${response.data.id}] был успешно сформирован. Скоро с вами свяжется оператор.`
            })
        }
        setLoading(false);
    }

    return <div className={styles.container}>
        <h2 className={styles.h2}>Корзина</h2>
        <div className={styles.container}>
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
    {

        (basket?.items !== undefined) && (basket.items.length > 0) && <div className={styles.container}>
            <div className={styles.footerItem}>
                <Button content='Оформить заказ' className={styles.button} onClick={handleCreateOrder} isLoading={isLoading} />
            </div>
            <div className={styles.footerPrice}>
                <div className={styles.priceContainer}>Итого: {basket.totalPrice} руб.</div>
            </div>
        </div>}
    </div>
});

export default BasketPage;