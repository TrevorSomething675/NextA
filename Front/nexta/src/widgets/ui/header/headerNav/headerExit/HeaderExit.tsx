import { ClearAuthStore } from "../../../../../shared/lib/authStorage";
import authStore from "../../../../../shared/stores/auth/authStore";
import styles from './HeaderExit.module.css';

export const HeaderExit = () => {
    const logout = () => {
        authStore.logout();
        ClearAuthStore();
    }

    return <span className={styles.exitBtn} onClick={logout}>
        Выйти
    </span>
}