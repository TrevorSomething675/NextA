import { observer } from 'mobx-react';
import basket from '../../../stores/basket';
import styles from './basketFooter.module.css';

const BasketFooter = observer(() => {
    return <div className={styles.container}>
        <div className={styles.footerItem}>
            <button className={styles.button}>Оформить заказ</button>
        </div>
        <div className={styles.footerPrice}>
            <div className={styles.priceContainer}>Итого: {basket.totalPrice} руб.</div>
        </div>
    </div>
});

export default BasketFooter;