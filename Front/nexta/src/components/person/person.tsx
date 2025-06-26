import styles from './person.module.css';
import auth from '../../stores/auth';

const Person = () => {
    return <div className={styles.container}>
        <div className={styles.personItem}>
            <div className={styles.personText}>
                Личный кабинет
            </div>
            <div className={styles.personData}>
                {auth.user.firstName} {auth.user.lastName} {auth.user.middleName}
             </div>   
        </div>
    </div>
}

export default Person;