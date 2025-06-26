import Image from '../../components/Image/Image';
import styles from './HeaderLogo2.module.css';

const HeaderLogo2 = () => {
    return <div className={styles.container}>
        <div className={styles.logoText}>
            БЕСПЛАТНЫЙ ПОДБОР: +7 915-562-95-13
        </div>
        <div className={styles.logoContainer}>
            <Image srcImage='/Logo1.jpg' isBase64Image={false} className={styles.logo}/>
        </div>
    </div>
}

export default HeaderLogo2;