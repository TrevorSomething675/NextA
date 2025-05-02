import News from '../news/news';
import Search from '../search/search';
import styles from './home.module.css';

const Home = () => {
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
</div>
}

export default Home;