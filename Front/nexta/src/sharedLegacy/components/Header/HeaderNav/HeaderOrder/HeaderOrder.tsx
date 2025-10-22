import { observer } from "mobx-react";
import styles from './HeaderOrder.module.css';

export const HeaderOrder = observer(() => {
    return <div className={styles.container}>
        <div className={styles.headerItem}>
            <svg xmlns="http://www.w3.org/2000/svg" 
                className={styles.svg} 
                width="2.2rem" height="2.2rem" 
                viewBox="0 0 16 16" 
                fill="none" stroke="currentColor">
                <rect x="2" y="6" width="12" height="9" rx="2" ry="2"/>
                <path d="M5 5V4a3 2.6 0 0 1 6 0v1"/>
            </svg>
            <div className={styles.text}>
                Заказы
            </div>
        </div>
    </div>
});