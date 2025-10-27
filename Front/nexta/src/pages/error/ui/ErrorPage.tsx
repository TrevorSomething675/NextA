import { useNavigate } from "react-router-dom";
import styles from './ErrorPage.module.css';
import Button from "../../../shared/ui/button/Button";

export const ErrorPage = () => {

    const navigation = useNavigate()

    const handleBackToHomePage = () => {
        navigation('/');
    }

    return <div className={styles.container}>
        <h2 className={styles.h2}>Вы попали не туда</h2>
        <div className={styles.text}>Такой страницы не существует</div>
        <Button className={styles.toHomeBtn} onClick={handleBackToHomePage}>
            На главную
        </Button>
    </div>
}