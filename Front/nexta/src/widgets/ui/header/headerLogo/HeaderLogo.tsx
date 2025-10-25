import { Link } from "react-router-dom";
import Image from "../../../../shared/ui/Image/Image";
import styles from './HeaderLogo.module.css';

const HeaderLogo = () => {
    return <div className={styles.container}>
        <Link to='/'>
            <Image srcImage='/logo.svg' isBase64Image={false} className={styles.logo} />
        </Link>
    </div>
}

export default HeaderLogo;