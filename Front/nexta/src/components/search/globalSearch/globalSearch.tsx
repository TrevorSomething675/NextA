import { useEffect, useRef, useState } from 'react';
import SvgSeachIcon from '../../svgs/searchIcon/SearchIcon';
import styles from './globalSearch.module.css';
import DetailsService from '../../../services/DetailsService';
import GetDetailsResponse from '../../../models/details/GetDetailsResponse';
import DetailsFilter from '../../../models/details/DetailsFilter';
import GetDetailsRequest from '../../../models/details/GetDetailsRequest';

interface GlobalSearchProps {
  onResponseChange: (response: GetDetailsResponse) => void;
}

const GlobalSearch: React.FC<GlobalSearchProps> = ({ onResponseChange }) => {
  const [isLoading, setLoading] = useState(false);
  const debounceTimeout = useRef<null | number>(null);
  const [response, setResponse] = useState<GetDetailsResponse>({} as GetDetailsResponse);

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setLoading(true);
    const value = e.target.value;

    if (debounceTimeout.current) {
      clearTimeout(debounceTimeout.current);
    }

    debounceTimeout.current = window.setTimeout(() => {
      fetchData(value);
    }, 1000);
  };

  const fetchData = async (query: string) => {
    const filter: DetailsFilter = {
      pageNumber: 1,
      pageSize: 16,
      searchTerm: query
    };
    const request: GetDetailsRequest = {
      filter
    };
    try {
      const response = await DetailsService.GetDetails(request);
      const newResponse = response;
      setResponse(newResponse);
      onResponseChange(newResponse);
    } catch (error) {
      console.error('Ошибка при получении данных:', error);
    }
    setLoading(false);
  };

  useEffect(() => {
    fetchData('');
  }, []);

  return (
    <div className={styles.container}>
      <button className={styles.searchButton}>
        <SvgSeachIcon />
      </button>
      <input
        className={styles.searchInput}
        placeholder="Введите артикул или название запчасти"
        onChange={handleInputChange}
      />
      <div className={styles.loadingContainer}>
        {isLoading && <img src="/loading.gif" className={styles.loading} />}
      </div>
    </div>
  );
};

export default GlobalSearch;