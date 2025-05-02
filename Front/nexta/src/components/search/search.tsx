import SvgSeachIcon from '../svgs/searchIcon/SearchIcon';
import styles from './search.module.css';

const Search = () => {
    return <div className={styles.container}>
        <button className={styles.searchButton}>
            <SvgSeachIcon />
        </button>
    <input className={styles.searchInput} placeholder="Введите артикул или название запчасти"/>
</div>
}

export default Search;