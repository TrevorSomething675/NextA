import styles from './headerNav.module.css';
import { useEffect } from 'react';
import auth from '../../stores/auth';
import { Link } from 'react-router-dom';
import { observer } from 'mobx-react';
import HeaderOrder from './headerOrder/headerOrder';
import HeaderBasket from './headerBasket/headerBasket';
import HeaderAccount from './headerAccount/headerAccount';
import HeaderExit from './headerExit/headerExit';
import HeaderAuth from './headerAuth/headerAuth';

const HeaderNav = observer(() => {
    useEffect(() => {
        const checkAuth = async () => {
            await auth.checkAuth();
            auth.setAuth(auth.isAuth);
        };
        checkAuth();
    }, []);

    return <div className={styles.container}>
        {auth.isAuth ? <>
            <div className={styles.accountItem}>
                <Link to='/Order'>
                    <HeaderOrder />
                </Link>
            </div>
            <div className={styles.accountItem}>
                <Link to='/Basket'>
                    <HeaderBasket />
                </Link>
            </div>
            <div className={styles.accountItem}>
                <Link to='/Account'>
                    <HeaderAccount />
                </Link>
            </div>
            <div className={styles.accountItem}>
                <Link to='/'>
                    <HeaderExit />
                </Link>
            </div>
        </> : <>
            <div className={styles.authItem}>
                <Link to='/Auth'>
                    <HeaderAuth />
                </Link>
            </div>
        </>}
    </div>
});

export default HeaderNav;