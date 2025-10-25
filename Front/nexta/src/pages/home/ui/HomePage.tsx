import styles from './HomePage.module.css';

export const HomePage = () => {
        return <div className={styles.container}>
        <div className={styles.newsBody}>
            <div className={styles.titleContainer}>
                <h2 className={styles.h2}>Новости</h2>
            </div>
        </div>
        <div className={styles.newsBody}>
            <div className={styles.titleContainer}>
                <h2 className={styles.h2}>Список товаров</h2>
            </div>
        </div>
    </div>
}