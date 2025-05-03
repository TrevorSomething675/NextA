import Details from '../../components/details/details';
import News from '../../components/news/news';
import Search from '../../components/search/search';
import styles from './home.module.css';

const HomePage = () => {
    return <div className={styles.container}>
    <div className={styles.header}>
        <Search />
    </div>
    <div className={styles.newsBody}>
        <div className={styles.titleContainer}>
            <h2 className={styles.h2}>Новости</h2>
        </div>
    </div>
    <News />
    <div className={styles.newsBody}>
        <div className={styles.titleContainer}>
            <h2 className={styles.h2}>Список деталей</h2>
        </div>
    </div>
    <Details />
</div>
}

export default HomePage;