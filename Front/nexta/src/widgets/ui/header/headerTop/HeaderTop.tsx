import { Link } from 'react-router-dom';
import authStore from '../../../../shared/stores/auth/authStore';
import { HeaderSearch } from '../../search/ui/headerSearch/HeaderSearch';
import HeaderLogo from '../headerLogo/HeaderLogo';
import { HeaderBasket } from '../headerNav/headerBasket/HeaderBasket';
import styles from './HeaderTop.module.css';
import { HeaderOrder } from '../headerNav/headerOrder/HeaderOrder';
import { HeaderAuth } from '../headerNav/headerAuth/HeaderAuth';
import basket from '../../../../stores/basket';

export const HeaderTop = () => {
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
}