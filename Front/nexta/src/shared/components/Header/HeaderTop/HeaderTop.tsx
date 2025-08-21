import { Link } from 'react-router-dom';
import { HeaderSearch } from '../../../../features/details/components/Search/HeaderSearch/HeaderSearch';
import HeaderLogo from '../../../assets/HeaderLogo/HeaderLogo';
import { HeaderBasket } from '../HeaderNav/HeaderBasket/HeaderBasket';
import { HeaderOrder } from '../HeaderNav/HeaderOrder/HeaderOrder';
import styles from './HeaderTop.module.css';
import authStore from '../../../../stores/AuthStore/authStore';
import { HeaderAuth } from '../HeaderNav/HeaderAuth/HeaderAuth';
import { observer } from 'mobx-react';

export const HeaderTop = observer(() => {
    return <div className={styles.container}>
        <div className={styles.header}>
            <HeaderLogo />
            <HeaderSearch />
            <div className={styles.storeContainer}>
                {authStore.isAuthenticated ? <>
                    <Link to='/Basket'>
                        <HeaderBasket />
                    </Link>
                    <Link to='/Order'>
                        <HeaderOrder />
                    </Link>
                </> : <>
                    <Link to='/Auth'>
                        <HeaderAuth />
                    </Link>
                </>}
            </div>
        </div>
    </div>
});