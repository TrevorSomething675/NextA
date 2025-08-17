import { observer } from "mobx-react";
import styles from './HeaderOrder.module.css';
import orderStore from "../../../../../stores/orderStore";

export const HeaderOrder = observer(() => {
    return <div className={styles.container}>
        <svg xmlns="http://www.w3.org/2000/svg" filter="drop-shadow(1px 1px 10px var(--black-color))" fill="currentColor" width="2.2rem" height="2.2rem" viewBox="0 0 16 16">
            <path d="M8 1a2.5 2.5 0 0 1 2.5 2.5V4h-5v-.5A2.5 2.5 0 0 1 8 1m3.5 3v-.5a3.5 3.5 0 1 0-7 0V4H1v10a2 2 0 0 0 2 2h10a2 2 0 0 0 2-2V4zM2 5h12v9a1 1 0 0 1-1 1H3a1 1 0 0 1-1-1z"/>
        </svg>
        
        <div className={styles.headerItem}>
            <div className={styles.text}>
                Заказы
            </div>
            <div className={styles.text}>
                Кол-во: {orderStore?.totalOrders !== undefined && orderStore?.totalOrders}
            </div>
        </div>
    </div>
});