import styles from './sidebar.module.css';

const Sidebar = () => {
    return <div className={styles.container}>
        <h1 className={styles.h1}>
            Наши контакты:
        </h1>
        <div className={styles.sidebarItem}>
            8 800 555 35 35
        </div>
        <h1 className={styles.h1}>
            Подписывайтесь на нас:
        </h1>
        <div className={styles.sidebarItem}>
            Telegram
        </div>
        <div className={styles.sidebarItem}>
            VK
        </div>
        <h1 className={styles.h1}>
            Вы можете найти нас по адресу:
        </h1>
        <div>
            Старый Оскол, мкр. восточный
        </div>
        <h1 className={styles.h1}>
            График работы:
        </h1>
        <div>
            9:00 - 18:00 пн-пт
        </div>
    </div>
}

export default Sidebar;