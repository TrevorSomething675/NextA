import { AuthPanel } from '../../../widgets/ui/auth-panel/ui/AuthPanel';
import styles from './AuthPage.module.css';

export const AuthPage = () => {
    return <div className={styles.container}>
        <AuthPanel />
    </div>
}