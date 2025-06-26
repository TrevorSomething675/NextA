import Search from "../../details/components/Search/Search/Search";
import WarehouseDetails from "../../details/components/WarehouseDetails/WarehouseDetails";
import News from "../../news/components/News/News";
import styles from './HomePage.module.css';

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
    <WarehouseDetails />
</div>
}

export default HomePage;