import { observer } from "mobx-react";
import { useNavigate } from "react-router-dom";
import { useNotifications } from "../../../shared/contexts/notifications/NotificationsContext";
import { useState } from "react";
import authStore from "../../../shared/stores/auth/authStore";
import basketStore from "../../../shared/stores/basket/basketStore";
import orderStore from "../../../shared/stores/order/orderStore";
import styles from './Basket.module.css';
import { Button } from "../../../shared/ui";
import { OrderApi } from "../../../shared/http/order/orderApi";
import { OrderItem } from "../../../entities/order/models/orderItem";
import { BasketProductItem } from "../../../widgets/ui/basket/basketProductItem/ui/BasketProductItem";

export const BasketContainer = observer(() => {
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
            
            const newOrdersResponse = await OrderApi.GetByUserId(userId);

            if(newOrdersResponse.success && newOrdersResponse.status === 200){
                orderStore.setOrderItems(newOrdersResponse.data.items);
            }  
            
            navigate('/Order');
            addNotification({
                header: 'Заказ сформирован',
                body: `Ваш заказ [${response.data}] был успешно сформирован. Скоро с вами свяжется оператор.`
            })
        }
        setLoading(false);
    }

    return <div className={styles.container}>
        <h2 className={styles.h2}>Корзина</h2>
        <div className={styles.container}>
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
    {

        (basketStore?.items !== undefined) && (basketStore.items.length > 0) && <div className={styles.container}>
            <div className={styles.footerItem}>
                <Button content='Оформить заказ' className={styles.button} onClick={handleCreateOrder} />
            </div>
            <div className={styles.footerPrice}>
                <div className={styles.priceContainer}>Итого: {basketStore.totalPrice} руб.</div>
            </div>
        </div>}
    </div>
});