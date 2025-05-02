import styles from './header.module.css';

import AccountHeader from '../accountHeader/accountHeader';
import Logo2 from '../logo2/logo2';
import Logo from '../logo/logo';

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