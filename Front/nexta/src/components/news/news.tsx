import Image from 'next/image';
import styles from './news.module.css';

const News = () => {
    return <div className={styles.container}>
        <ul className={styles.ul}>
            <li className={styles.newsItem}>
                <Image src='/news1.jpg' alt='' width={500} height={400} className={styles.img} />
            </li>
            <li className={styles.newsItem}>
                <Image src='/news2.jpg' alt='' width={500} height={400} className={styles.img} />
            </li>
            <li className={styles.newsItem}>
                <Image src='/news3.jpg' alt='' width={500} height={400} className={styles.img} />
            </li>
        </ul>
    </div>
}

export default News;