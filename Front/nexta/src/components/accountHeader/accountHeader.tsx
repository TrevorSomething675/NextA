import Link from 'next/link';
import BasketMini from '../basketMini/basketMini';
import Person from '../person/person';
import styles from './accountHeader.module.css';
import LoginSvg from '../svgs/loginSvg/loginSvg';

import { useEffect, useState } from 'react';
import auth from '@/stores/auth';
import ExitSvg from '../svgs/exitSvg/exitSvg';
import { useRouter } from 'next/navigation';

const AccountHeader = () => {
    const [isAuth, SetAuth] = useState(auth.isAuth)
    const router = useRouter();

    const logout = () => {
        auth.logout();
        SetAuth(false);
        router.push('/');
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
                <Link href="/basket">
                    <BasketMini />
                </Link>
            </div>
            <div className={styles.accountItem}>
                <Link href="/account">
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
        <div className={styles.authItem}>
            <LoginSvg />
            <button className={styles.authBtn}>
                <Link href="/auth">
                    Войти
                </Link>
            </button>
        </div>
        </>}
    </div>
}

export default AccountHeader;