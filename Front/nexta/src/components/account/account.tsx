import styles from './account.module.css';
import auth from '../../stores/auth';

const Account = () => {
    return <div className={styles.container}>
        <div className={styles.accountHeader}>
            <h2 className={styles.h2}>Личный кабинет</h2>
        </div>
        <div className={styles.accountBody}>
            <ul>
                <li className={styles.li}>ФИО: {auth.user.firstName} {auth.user.lastName} {auth.user.middleName}</li>
                <li className={styles.li}>Номер телефона: 8 800 555 35 35</li>
                <li className={styles.li}>Почта: {auth.user.email}</li>
                <button className={styles.changePassButton}>Изменить пароль</button>
            </ul>
        </div>
    </div>
}

export default Account;