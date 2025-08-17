import AdminSubHeader from "../../../features/admin/components/AdminSubHeader/AdminSubHeader";
import authStore from "../../../stores/AuthStore/authStore";
import styles from './Header.module.css';
import { HeaderBottom } from "./HeaderBottom/HeaderBottom";
import { HeaderTop } from "./HeaderTop/HeaderTop";

const Header:React.FC = () => {
    return <div className={styles.container}>
        <HeaderTop />
        <HeaderBottom />
        {authStore?.isAdmin && <AdminSubHeader />}
    </div>
}

export default Header;