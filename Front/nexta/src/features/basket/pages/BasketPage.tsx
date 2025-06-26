import BasketBody from '../components/BasketBody/BasketBody';
import BasketFooter from '../components/BasketFooter/BasketFooter';
import BasketHeader from '../components/BasketHeader/BasketHeader';
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