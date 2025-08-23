import basket from "../../../../../stores/basket";
import styles from './HeaderBasket.module.css';
import { observer } from "mobx-react";

interface Props {
    className?:string
}

export const HeaderBasket:React.FC<Props> = observer(({className}) => {
    const container = `${styles.container} ${className || ''}`.trim();

    return <div className={container}> 
        <svg xmlns="http://www.w3.org/2000/svg" filter="drop-shadow(1px 1px 10px var(--text-color))" fill="currentColor" width="2.1rem" height="2.1rem" viewBox="0 0 16 16">
            <path d="M0 1.5A.5.5 0 0 1 .5 1H2a.5.5 0 0 1 .485.379L2.89 3H14.5a.5.5 0 0 1 .491.592l-1.5 8A.5.5 0 0 1 13 12H4a.5.5 0 0 1-.491-.408L2.01 3.607 1.61 2H.5a.5.5 0 0 1-.5-.5M3.102 4l1.313 7h8.17l1.313-7zM5 12a2 2 0 1 0 0 4 2 2 0 0 0 0-4m7 0a2 2 0 1 0 0 4 2 2 0 0 0 0-4m-7 1a1 1 0 1 1 0 2 1 1 0 0 1 0-2m7 0a1 1 0 1 1 0 2 1 1 0 0 1 0-2"/>
        </svg>
        
        <div className={styles.headerItem}>
            <div className={styles.text}>
                Корзина
            </div>
            <div className={styles.text}>
                {basket.totalPrice} руб.
            </div>
        </div>
    </div> 
});