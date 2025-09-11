import AdminSubHeader from "../../../features/admin/components/AdminSubHeader/AdminSubHeader";
import authStore from "../../../stores/AuthStore/authStore";
import styles from './Header.module.css';
import { HeaderBottom } from "./HeaderBottom/HeaderBottom";
import { HeaderTop } from "./HeaderTop/HeaderTop";

export const Header = () => {
    return <div className={styles.container}>
        <HeaderTop />
        <HeaderBottom />
        {authStore?.isAdmin && <AdminSubHeader />}
    </div>
}