import styles from './SidebarStatusFilter.module.css';

export const SidebarStatusFilter = () => {
    
    return <div className={styles.container}>
        <div className={styles.statusItem}>
            <input
                className={styles.checkbox}
                type="checkbox"
                name="priceRange"
                value="under500"
                />
            <label htmlFor="range-under500">Есть в ПВЗ</label>
        </div>
        <div className={styles.statusItem}>
            <input
                className={styles.checkbox}
                type="checkbox"
                name="priceRange"
                value="under500"
            />
            <label htmlFor="range-under500">Скидка</label>
        </div>
    </div>
}