import { useNavigate } from "react-router-dom";
import basket from "../../../../../stores/basket";
import styles from './HeaderExit.module.css';
import authStore from "../../../../../stores/AuthStore/authStore";
import { AuthService } from "../../../../../features/auth/services/AuthService";

export const HeaderExit = () => {
    const navigate = useNavigate();

    const logout = () => {
        authStore.logout();
        AuthService.logout();
        basket.clear();
        navigate('/')
    }

    return <div className={styles.container}>
        <svg xmlns="http://www.w3.org/2000/svg" width="2.1rem" height="2.1rem" viewBox="0 0 16 16">
            <path d="M8.5 10c-.276 0-.5-.448-.5-1s.224-1 .5-1 .5.448.5 1-.224 1-.5 1"/>
            <path d="M10.828.122A.5.5 0 0 1 11 .5V1h.5A1.5 1.5 0 0 1 13 2.5V15h1.5a.5.5 0 0 1 0 1h-13a.5.5 0 0 1 0-1H3V1.5a.5.5 0 0 1 .43-.495l7-1a.5.5 0 0 1 .398.117M11.5 2H11v13h1V2.5a.5.5 0 0 0-.5-.5M4 1.934V15h6V1.077z"/>
        </svg>
        <button className={styles.exitBtn} onClick={logout}>
            Выйти
        </button>
    </div>
}