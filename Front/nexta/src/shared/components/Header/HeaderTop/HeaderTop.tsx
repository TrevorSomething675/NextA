import { Link } from 'react-router-dom';
import HeaderLogo from '../../../assets/HeaderLogo/HeaderLogo';
import { HeaderBasket } from '../HeaderNav/HeaderBasket/HeaderBasket';
import { HeaderOrder } from '../HeaderNav/HeaderOrder/HeaderOrder';
import styles from './HeaderTop.module.css';
import authStore from '../../../../stores/AuthStore/authStore';
import { HeaderAuth } from '../HeaderNav/HeaderAuth/HeaderAuth';
import { observer } from 'mobx-react';
import { HeaderSearch } from '../../../../features/search/components/HeaderSearch/HeaderSearch';
import basket from '../../../../stores/basket';

export const HeaderTop = observer(() => {
    const HandleOpenBasket = () => {
        basket.setVisibleBasket(true);
    }

    return <div className={styles.container}>
        <div className={styles.header}>
            <HeaderLogo />
            <HeaderSearch />
            <div className={styles.storeContainer}>
                {authStore.isAuthenticated ? <>
                    <HeaderBasket onClick={HandleOpenBasket}/>
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