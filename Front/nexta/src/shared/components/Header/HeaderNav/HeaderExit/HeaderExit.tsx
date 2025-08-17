import basket from "../../../../../stores/basket";
import styles from './HeaderExit.module.css';
import authStore from "../../../../../stores/AuthStore/authStore";
import { AuthService } from "../../../../../features/auth/services/AuthService";

export const HeaderExit = () => {
    const logout = () => {
        authStore.logout();
        AuthService.logout();
        basket.clear();
    }

    return <span className={styles.exitBtn} onClick={logout}>
        Выйти
    </span>
}