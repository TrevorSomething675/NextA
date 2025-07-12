import AdminNews from '../../components/AdminNews/AdminNews';
import CreateNews from '../../components/CreateNews/CreateNews';
import styles from './AdminNewsPage.module.css';

const AdminNewsPage = () => {
    return <div className={styles.container}>
        <h2 className={styles.h2}>Новости</h2>
        <div className={styles.newsSlider}>
            <AdminNews />
        </div>
        <h2 className={styles.h2}>Создать новость</h2>
        <div className={styles.createNewsContainer}>
            <CreateNews />
        </div>
    </div>
}

export default AdminNewsPage;