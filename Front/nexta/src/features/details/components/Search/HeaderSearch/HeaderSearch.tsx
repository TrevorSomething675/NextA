import { useEffect, useRef, useState } from "react";
import { SearchDetailsFilter, SearchDetailsRequest, SearchDetailsResponse } from "../../../models/SearchDetails";
import SearchSvg from "../../../svgs/SearchSvg/SearchSvg";
import SearchItem from "../SearchItem/SearchItem";
import styles from './HeaderSearch.module.css';
import { useNavigate } from "react-router-dom";
import DetailsService from "../../../../../services/DetailsService";

interface Props {
    className?:string
}

export const HeaderSearch:React.FC<Props> = ({className}) => {
    const container = `${styles.container} ${className || ''}`.trim();

    const containerRef = useRef<HTMLDivElement>(null);
    const debounceTimeout = useRef<null | number>(null);
    const [response, setResponse] = useState<SearchDetailsResponse>({} as SearchDetailsResponse);
    const [isLoading, setLoading] = useState(false);
    const navigator = useNavigate();

    const [inFocus, setFocus] = useState(false); 

    const handleFocus = () =>{
        setFocus(true);
    };
    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setLoading(true);
        const value = e.target.value;

    if(debounceTimeout.current) {
        clearTimeout(debounceTimeout.current);
    }

    debounceTimeout.current = setTimeout(() => {
        fetchData(value);
        }, 1000);
    };

    const goToSearchPage = () => {
        navigator('/Search');
    }

    const fetchData = async (query:string) => {
        if(query == ''){
            setResponse({} as SearchDetailsResponse);
        } 
        else {
            const filter:SearchDetailsFilter = {
            pageNumber: 1,
            searchTerm: query
        }
            const request:SearchDetailsRequest = {
            filter: filter
        }
            const response = await DetailsService.GetDetails(request);
            setResponse(response);
        }
        setLoading(false);
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

    return <div className={container} ref={containerRef}>
        <button className={styles.searchButton}>
            <SearchSvg />
        </button>
        <input
            className={styles.searchInput}
            placeholder="Введите артикул или название запчасти"
            onChange={handleInputChange}
            onFocus={handleFocus}
            />
        {inFocus &&
            <div className={styles.autoCompleteSearch}>
                <ul>
                    {response?.data?.items?.map((detail) => 
                        <SearchItem key={detail.id} detail={detail}/>
                    )}
                </ul>
                <hr className={styles.hr}/>
                <div className={styles.autoCompleteFooter}>
                <button className={styles.globalSearchBtn} onClick={goToSearchPage}>
                    Глобальный поиск 
                </button>
                {(response?.data?.items?.length == 0) && 
                <div className={styles.redColor}>
                    Ничего не найдено
                </div>
                }
            </div>
        </div>
      }
        <div className={styles.loadingContainer}>
            {isLoading && <img src="/loading.gif" className={styles.loading}/>}
        </div>
    </div>
};