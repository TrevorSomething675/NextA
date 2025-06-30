import AdminSubHeader from "../../../features/admin/components/AdminSubHeader/AdminSubHeader";
import HeaderLogo1 from "../../assets/HeaderLogo1/HeaderLogo1";
import HeaderLogo2 from "../../assets/HeaderLogo2/HeaderLogo2";
import styles from './Header.module.css';
import { HeaderNav } from "./HeaderNav/HeaderNav";

const Header:React.FC = () => {
    return <div className={styles.container}>
        <div className={styles.header}>
            <div className={styles.headerItems}>
                <HeaderLogo1 />
                <HeaderLogo2 />
                <HeaderNav />
            </div>
        </div>
        <AdminSubHeader />
    </div>
}

export default Header;