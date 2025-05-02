import React from 'react';
import styles from './search.module.css';
import SearchIcon from '@/components/svgs/searchIcon/SearchIcon';

const Search = () => {
    return <div className={styles.container}>
        <button className={styles.searchButton}>
            <SearchIcon />
        </button>
    <input className={styles.searchInput} placeholder="Введите артикул или название запчасти"/>
</div>
}

export default Search;