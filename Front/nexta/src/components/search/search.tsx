import { useEffect, useRef, useState } from 'react';
import SvgSeachIcon from '../svgs/searchIcon/SearchIcon';
import styles from './search.module.css';
import SearchService from '../../services/SearchService';
import SearchDetailFilter from '../../models/search/SearchDetailFilter';
import SearchDetailRequest from '../../models/search/SearchDetailRequest';
import SearchDetailResponse from '../../models/search/SearchDetailResponse';
import SearchItem from './searchItem/searchItem';

const Search = () => {
  const containerRef = useRef<HTMLDivElement>(null);
  const debounceTimeout = useRef<null | number>(null);
  const [response, setDetails] = useState<SearchDetailResponse>({} as SearchDetailResponse);

  const [inFocus, setFocus] = useState(false); 

  const handleFocus = () =>{
    setFocus(true);
  };
  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;

    if(debounceTimeout.current) {
      clearTimeout(debounceTimeout.current);
    }

    debounceTimeout.current = setTimeout(() => {
        fetchData(value);
      }, 1000);
  };

  const fetchData = async (query:string) => {
    if(query == ''){
      setDetails({} as SearchDetailResponse);
    } 
    else {
      const filter:SearchDetailFilter = {
        pageNumber: 1,
        searchTerm: query
      }
      const request:SearchDetailRequest = {
        filter: filter
      }
      const details = await SearchService.SearchDetail(request);
      setDetails(details.value);
    }
  };

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (containerRef.current && !containerRef.current.contains(event.target as Node)) {
        setFocus(false);
      }
    };

    document.addEventListener('mousedown', handleClickOutside);

    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, []);

  return <div className={styles.container} ref={containerRef}>
      <button className={styles.searchButton}>
        <SvgSeachIcon />
      </button>
      <input
        className={styles.searchInput}
        placeholder="Введите артикул или название запчасти"
        onChange={handleInputChange}
        onFocus={handleFocus}
        />
      {(response.details !== undefined) && (inFocus) && 
        <div className={styles.autoCompleteSearch}>
            {response?.details?.items?.map((detail) => 
              <SearchItem key={detail.id} detail={detail}/>
            )}
            <div className={styles.autoCompleteFooter}>
              Страница поиска
            </div>
        </div>
      }
    </div>
};

export default Search;