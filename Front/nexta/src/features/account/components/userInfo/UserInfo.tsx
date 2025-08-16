import { observer } from 'mobx-react';
import authStore from '../../../../stores/AuthStore/authStore';
import styles from './UserInfo.module.css';

export const UserInfo = observer(() => {
    return <div>
        <h2 className={styles.h2}>Ваши данные</h2>
        <div className={styles.userContainer}>
            <ul className={styles.ul}>
                <li className={styles.li}>Имя: {authStore?.user?.firstName}</li>
                <li className={styles.li}>Фамилия: {authStore?.user?.lastName}</li>
                <li className={styles.li}>Отчество: {authStore?.user?.middleName}</li>
            </ul>
            <ul className={styles.ul}>
                <li className={styles.li}>E-mail: {authStore?.user?.email}</li>
                <li className={styles.li}>Номер телефона: {(authStore.user.phone) ? authStore.user.phone : <span className={styles.error}>Отсутствует</span>}</li>
            </ul>
        </div>
    </div>
});