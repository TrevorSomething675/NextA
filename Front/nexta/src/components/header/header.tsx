import styles from './header.module.css';

import Logo2 from '../logo2/logo2';
import Logo from '../logo/logo';
import HeaderNav from '../headerNav/headerNav';

const Header:React.FC = () => {
    return <div className={styles.header}>
        <div className={styles.headerItems}>
            <Logo />
            <Logo2 />
            <HeaderNav />
        </div>
    </div>
}

export default Header;