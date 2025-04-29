import styles from './footer.module.css';
import VkSvg from '../svgs/vkSvg/vkSvg';
import Image from 'next/image'

const Footer:React.FC = () => {
    return <div className={styles.footer}>
        <ul className={styles.ul}>
            <li className={styles.li}>
                Наши контакты: +7 915-562-95-13
            </li>
            <li className={styles.li}>
                Наш адрес: г. Старый Оскол мкр. Восточный дом 49
            </li>
            <li className={styles.li}>
                Подписывайтесь на нас: 
                <a href='https://vk.com/avtozapchasti.nextauto' className={styles.a}>
                    <Image src="/Vkontakte.png" alt="" width={300} height={300} className={styles.img} />
                </a>
                <a href='https://www.avito.ru/brands/4c06fa716346b81ea1c4222fc5f723de/items/all?sellerId=de902dd30007e484ec1eb57c99bf8407&s=search_page_share' className={styles.a}>
                    <Image src="/Avito.png" alt="" width={300} height={300} className={styles.img} />
                </a>
            </li>
        </ul>
    </div>
}

export default Footer;