import { observer } from 'mobx-react';
import styles from './basket.module.css';
import BasketHeader from '../../components/basket/basketHeader/basketHeader';
import BasketBody from '../../components/basket/basketBody/basketBody';
import BasketFooter from '../../components/basket/basketFooter/basketFooter';

const BasketPage = observer(() => {
    return <div className={styles.container}>
        <BasketHeader />
        <BasketBody />
        <BasketFooter />
    </div>
});

export default BasketPage;