import { Link } from 'react-router-dom';
import styles from './HeaderLogo.module.css';
import Image from '../../components/Image/Image';

const HeaderLogo = () => {
    return <div className={styles.container}>
        <Link to='/'>
            <Image srcImage='/logo.svg' isBase64Image={false} className={styles.logo} />
        </Link>
    </div>
}

export default HeaderLogo;