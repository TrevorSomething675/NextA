import BasketMini from '../basketMini/basketMini';
import Person from '../person/person';
import styles from './accountHeader.module.css';
import LoginSvg from '../svgs/loginSvg/loginSvg';
import { useEffect, useState } from 'react';
import ExitSvg from '../svgs/exitSvg/exitSvg';
import auth from '../../stores/auth';
import { Link, useNavigate } from 'react-router-dom';

const AccountHeader = () => {
    const [isAuth, SetAuth] = useState(auth.isAuth)
    const navigate = useNavigate();

    const logout = () => {
        auth.logout();
        SetAuth(false);
        navigate('/')
    }

    useEffect(() => {
        const checkAuth = async () => {
            await auth.checkAuth();
            SetAuth(auth.isAuth);
        };
        
        checkAuth();
    }, []);

    return <div className={styles.container}>
        {isAuth ? <>
            <div className={styles.accountItem}>
                    <BasketMini />
            </div>
            <div className={styles.accountItem}>
                    <Person />
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
}

export default AccountHeader;