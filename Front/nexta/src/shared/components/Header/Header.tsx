import HeaderLogo1 from "../../assets/HeaderLogo1/HeaderLogo1";
import HeaderLogo2 from "../../assets/HeaderLogo2/HeaderLogo2";
import styles from './Header.module.css';

const Header:React.FC = () => {
    return <div className={styles.header}>
        <div className={styles.headerItems}>
            <HeaderLogo1 />
            <HeaderLogo2 />
            <Header />
        </div>
    </div>
}

export default Header;