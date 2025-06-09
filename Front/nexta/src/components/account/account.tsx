import styles from './account.module.css';
import auth from '../../stores/auth';
import { observer } from 'mobx-react';
import LegacyOrders from '../order/legacyOrders';

const Account = observer(() => {
    return <div className={styles.container}>
        <h2 className={styles.h2}>Личный кабинет</h2>
        <div className={styles.accountHeader}>
            <ul>
                <li className={styles.li}>ФИО: {auth?.user?.firstName} {auth?.user?.lastName} {auth?.user?.middleName}</li>
                <li className={styles.li}>
                    Номер телефона: 
                    <span className={`${!auth?.user?.phone && styles.numberIsNull}`}>
                        {auth?.user?.phone ? auth?.user?.phone : 'Отсутствует'}
                    </span>
                    </li>
                <li className={styles.li}>Почта: {auth.user.email}</li>
                <button className={styles.changePassButton}>Изменить пароль</button>
            </ul>
        </div>
        <h2 className={styles.h2}>История заказов</h2>
        <div className={styles.accountBody}>
            <LegacyOrders />
        </div>
    </div>
});

export default Account;