import { Link } from 'react-router-dom';
import authStore from '../../../../stores/AuthStore/authStore';
import styles from './AdminSubHeader.module.css';

const AdminSubHeader = () => {

    if(!authStore?.isAdmin){
        return null;
    }

    return <div className={styles.container}>
        <div className={styles.subHeaderItems}>
            <div className={styles.subHeaderItem}>
                <Link className={styles.link} to='/Admin/Orders'>
                    Заказы
                </Link>
            </div>
            <div className={styles.subHeaderItem}>
                <Link className={styles.link} to='/Admin/Products'>
                    Детали
                </Link>
            </div>
            <div className={styles.subHeaderItem}>
                <Link className={styles.link} to='/Admin/News'>
                    Новости
                </Link>
            </div>
        </div>
    </div>
}

export default AdminSubHeader;