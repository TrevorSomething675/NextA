import News from "../../news/components/News/News";
import { ProductsContainer } from "../../product/components/ProductsContainer/ProductsContainer";
import styles from './HomePage.module.css';

const HomePage = () => {
    return <div className={styles.container}>
    <div className={styles.newsBody}>
        <div className={styles.titleContainer}>
            <h2 className={styles.h2}>Новости</h2>
        </div>
    </div>
    <News />
    <div className={styles.newsBody}>
        <div className={styles.titleContainer}>
            <h2 className={styles.h2}>Список товаров</h2>
        </div>
    </div>
    <ProductsContainer />
</div>
}

export default HomePage;