import { useNavigate } from 'react-router-dom';
import Button from '../../../shared/components/Button/Button';
import styles from './ErrorPage.module.css';

export const ErrorPage = () => {

    const navigation = useNavigate()

    const handleBackToHomePage = () => {
        navigation('/');
    }

    return <div className={styles.container}>
        <h2 className={styles.h2}>Вы попали не туда</h2>
        <div className={styles.text}>Такой страницы не существует</div>
        <Button content='На главную страницу' className={styles.toHomeBtn} onClick={handleBackToHomePage}/>
    </div>
}