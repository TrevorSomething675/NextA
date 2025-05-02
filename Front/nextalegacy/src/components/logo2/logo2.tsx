import styles from './logo2.module.css';
import Image from 'next/image'

const Logo2 = () => {
    return  <div className={styles.container}>
            <div className={styles.logoText}>БЕСПЛАТНЫЙ ПОДБОР: +7 915-562-95-13</div>
                <div className={styles.logoContainer}>
                <Image src="/Logo1.jpg" alt="" width={1000} height={500} className={styles.logo} />
            </div>
        </div>
}

export default Logo2;