import { useEffect, useRef, useState } from "react";
import { GetDetailsFilter, GetDetailsRequest, GetDetailsResponse } from "../../../models/GetDetails";
import DetailsService from "../../../../../services/DetailsService";
import SearchSvg from "../../../svgs/SearchSvg/SearchSvg";
import styles from './GlobalSearch.module.css';

interface GlobalSearchProps {
    onResponseChange: (response: GetDetailsResponse) => void;
}

export const GlobalSearch: React.FC<GlobalSearchProps> = ({ onResponseChange }) => {
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
        const filter: GetDetailsFilter = {
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
                <SearchSvg />
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