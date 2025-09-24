import AdminSubHeader from "../../../features/admin/components/AdminSubHeader/AdminSubHeader";
import authStore from "../../../stores/AuthStore/authStore";
import styles from './Header.module.css';
import { HeaderBottom } from "./HeaderBottom/HeaderBottom";

export const Header = () => {
    return <div className={styles.container}>
        <HeaderBottom />
        {authStore?.isAdmin && <AdminSubHeader />}
    </div>
}