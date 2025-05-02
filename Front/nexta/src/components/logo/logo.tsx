import { Link } from 'react-router-dom';
import Image from '../image/Image';
import styles from './logo.module.css';

const Logo = () => {
    return <div className={styles.container}>
        <Link to='/'>
            <Image srcImage='/nextaLogo.jpg' isBase64Image={false} />
        </Link>
    </div>
}

export default Logo;