import Image from '../image/Image';
import styles from './news.module.css';

const News = () => {
    return <div className={styles.container}>
        <ul className={styles.ul}>
        <li className={styles.newsItem}>
                <Image srcImage='/news1.jpg' className={styles.img}/>
            </li>
            <li className={styles.newsItem}>
                <Image srcImage='/news2.jpg' className={styles.img}/>
            </li>
            <li className={styles.newsItem}>
                <Image srcImage='/news3.jpg' className={styles.img}/>
            </li>
        </ul>
    </div>
}

export default News;