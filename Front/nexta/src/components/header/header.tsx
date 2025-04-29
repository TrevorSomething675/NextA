'use client'

import styles from './header.module.css';

import Logo from '@/components/logo/logo';
import AccountHeader from '../accountHeader/accountHeader';
import Logo2 from '../logo2/logo2';

const Header:React.FC = () => {
    return <div className={styles.header}>
        <div className={styles.headerItems}>
            <Logo />
            <Logo2 />
            <AccountHeader /> 
        </div>
    </div>
}

export default Header;