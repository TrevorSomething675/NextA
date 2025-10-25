import authStore from "../../../shared/stores/auth/authStore"
import AdminSubHeader from "../admin-panel/ui/adminSubHeader/AdminSubHeader"
import { HeaderBottom } from "./headerBottom/HeaderBottom"
import styles from './Header.module.css';

export const Header = () => {
    return <div className={styles.container}>
        <HeaderBottom />
        {authStore?.isAdmin && <AdminSubHeader />}
    </div>
}