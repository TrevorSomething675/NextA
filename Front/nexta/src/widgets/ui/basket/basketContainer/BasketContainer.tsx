import { observer } from "mobx-react";
import authStore from "../../../../shared/stores/auth/authStore";
import basketStore from "../../../../shared/stores/basket/basketStore";
import { OrderItem } from "../../../../entities/order/models/orderItem";
import styles from './BasketContainer.module.css';
import { Button } from "../../../../shared/ui";
import { BasketProductItem } from "../basketProductItem/BasketProductItem";
import { useCreateOrder } from "../../../../features/order/createOrder/useCreateOrder";

export const BasketContainer = observer(() => {
    const { createOrder } = useCreateOrder();

    const handleCreateOrder = () =>{
        fetchData();
    }
    
    const fetchData = async() => {
        const userId = authStore?.user?.id ?? '';
        const products = basketStore.items.map((product) => ({
            productId: product.productId,
            product: product.product,
            count: product.count
        }) as OrderItem);

        await createOrder(userId, products);
    }

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
            <tbody>
                {basketStore.items.length > 0 && basketStore.items.map((product) => 
                    <BasketProductItem basketProduct={product} key={product.productId} />
                )}
            </tbody>
        </table>)
        :
        (<div className={styles.noBasketProducts}>
            Ваша корзина пуста.
        </div>)}
        {(basketStore?.items !== undefined) && (basketStore.items.length > 0) && <div className={styles.footer}>
                <Button className={styles.button} onClick={handleCreateOrder}>
                    Оформить заказ
                </Button>
                <div className={styles.priceContainer}>
                    Итого: {basketStore.totalPrice} руб.
                </div>
        </div>}
    </div>
});