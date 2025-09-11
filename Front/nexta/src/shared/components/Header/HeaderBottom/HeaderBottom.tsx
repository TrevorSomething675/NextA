import { Link } from 'react-router-dom';
import styles from './HeaderBottom.module.css';
import { HeaderExit } from '../HeaderNav/HeaderExit/HeaderExit';
import authStore from '../../../../stores/AuthStore/authStore';
import { observer } from 'mobx-react';

export const HeaderBottom = observer(() => {
    return <div className={styles.container}>
        <div className={styles.header}>
            <div>
                <Link to='/' className={styles.headerItem}>
                    Главная
                </Link>
                <Link to='/Search' className={styles.headerItem}>
                    Товары
                </Link>
            </div>
            <div>
                Бесплатный подбор: +7 (915) 562-95-13
            </div>
            <div>
                {authStore.isAuthenticated &&
                    <>
                        <Link to='/Account' className={styles.headerItem}>
                            Личный кабинет
                        </Link>
                        <Link to='/' className={styles.headerItem}>
                            <HeaderExit />
                        </Link>
                    </>
                }
            </div>
        </div>
    </div>
});