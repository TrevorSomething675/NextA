import { observer } from 'mobx-react';
import basket from '../../../stores/basket';
import styles from './basketFooter.module.css';

const BasketFooter = observer(() => {
    return <div className={styles.container}>
        <button className={styles.button}>Оформить заказ</button>
        <div className={styles.priceContainer}>Итоговая стоимость: {basket.totalPrice} руб.</div>
    </div>
});

export default BasketFooter;