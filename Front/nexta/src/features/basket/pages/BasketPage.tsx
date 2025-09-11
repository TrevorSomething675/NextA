import BasketBody from '../components/Basket/BasketBody/BasketBody';
import BasketFooter from '../components/Basket/BasketFooter/BasketFooter';
import BasketHeader from '../components/Basket/BasketHeader/BasketHeader';
import styles from './BasketPage.module.css';
import { observer } from "mobx-react";

const BasketPage = observer(() => {
    return <div className={styles.container}>
        <BasketHeader />
        <BasketBody />
        <BasketFooter />
    </div>
});

export default BasketPage;