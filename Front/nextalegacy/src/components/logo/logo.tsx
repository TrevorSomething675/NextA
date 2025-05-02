import Link from 'next/link';
import styles from './logo.module.css';
import Image from 'next/image'

const Logo = () => {
    return <div>
        <Link href="/">
            <Image src="/nextaLogo.jpg" alt="" width={300} height={100} className={styles.img}/>
        </Link>
    </div>
}

export default Logo;