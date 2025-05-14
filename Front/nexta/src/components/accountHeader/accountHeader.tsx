import BasketMini from '../basketMini/basketMini';
import Person from '../person/person';
import styles from './accountHeader.module.css';
import LoginSvg from '../svgs/loginSvg/loginSvg';
import { useEffect } from 'react';
import ExitSvg from '../svgs/exitSvg/exitSvg';
import auth from '../../stores/auth';
import { Link, useNavigate } from 'react-router-dom';
import { observer } from 'mobx-react';
import basket from '../../stores/basket';

const AccountHeader = observer(() => {
    const navigate = useNavigate();

    const logout = () => {
        auth.logout();
        auth.setAuth(false);
        basket.clear();
        navigate('/')
    }

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
                <Link to='/Basket'>
                    <BasketMini />
                </Link>
            </div>
            <div className={styles.accountItem}>
                <Link to='/Account'>
                    <Person />
                </Link>
            </div>
            <div className={styles.accountItem}>
                <button className={styles.exitBtn} onClick={logout}>
                    <ExitSvg />
                    Выйти
                </button>
            </div>
        </> : <>
        <Link to='/Auth'>
            <div className={styles.authItem}>
                <LoginSvg />
                <button className={styles.authBtn}>
                    Войти
                </button>
            </div>
        </Link>
        </>}
    </div>
});

export default AccountHeader;