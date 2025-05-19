import { useEffect, useRef, useState } from 'react';
import SvgSeachIcon from '../svgs/searchIcon/SearchIcon';
import styles from './search.module.css';
import SearchService from '../../services/SearchService';
import SearchDetailFilter from '../../models/search/SearchDetailFilter';
import SearchDetailRequest from '../../models/search/SearchDetailRequest';

const Search = () => {
  const [searchTerm, setSearchTerm] = useState('');
  const debounceTimeout = useRef<null | number>(null);

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    setSearchTerm(value);

    if (debounceTimeout.current) {
      clearTimeout(debounceTimeout.current);
    }

    debounceTimeout.current = setTimeout(() => {
      fetchData(value);
    }, 1000);
  };

  const fetchData = async (query:string) => {
    const filter:SearchDetailFilter = {
      pageNumber: 1,
      searchTerm: query
    }
    const request:SearchDetailRequest = {
      filter: filter
    }
    const details = await SearchService.SearchDetail(request);
    console.log(details.value);
  };

  useEffect(() => {
    return () => {
      if (debounceTimeout.current) {
        clearTimeout(debounceTimeout.current);
      }
    };
  }, []);

  return (
    <div className={styles.container}>
      <button className={styles.searchButton}>
        <SvgSeachIcon />
      </button>
      <input
        className={styles.searchInput}
        placeholder="Введите артикул или название запчасти"
        value={searchTerm}
        onChange={handleInputChange}
      />
    </div>
  );
};

export default Search;