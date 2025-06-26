import { Link } from 'react-router-dom';
import styles from './HeaderLogo1.module.css';
import Image from '../../components/Image/Image';

const HeaderLogo1 = () => {
    return <div className={styles.container}>
        <Link to='/'>
            <Image srcImage='/nextaLogo.jpg' isBase64Image={false} />
        </Link>
    </div>
}

export default HeaderLogo1;