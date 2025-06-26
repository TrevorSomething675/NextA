import { observer } from "mobx-react";
import styles from './Account.module.css';
import LegacyOrders from "../../../order/components/legacyOrders/LegacyOrders";
import authStore from "../../../../stores/AuthStore/authStore";

const Account = observer(() => {
    return <div className={styles.container}>
        <h2 className={styles.h2}>Личный кабинет</h2>
        <div className={styles.accountHeader}>
            <ul>
                <li className={styles.li}>
                    <h2>
                        {authStore?.user?.firstName} {authStore?.user?.lastName} {authStore?.user?.middleName}
                    </h2>
                </li>
                <li className={styles.li}>
                    Номер телефона:
                    <span className={`${!authStore?.user?.phone && styles.numberIsNull}`}>
                        {authStore?.user?.phone ? authStore?.user?.phone : 'Отсутствует'}
                    </span>
                    </li>
                <li className={styles.li}>Почта: {authStore.user.email}</li>
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