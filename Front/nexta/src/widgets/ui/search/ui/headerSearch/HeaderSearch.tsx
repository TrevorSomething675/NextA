import { useEffect, useRef, useState } from 'react';
import styles from './HeaderSearch.module.css';
import { useSearchProductsStore } from '../../../../../shared/stores/searchProduct/searchProductsStore';
import SearchSvg from '../../../svg/SearchSvg/SearchSvg';
import authStore from '../../../../../shared/stores/auth/authStore';
import { useNavigate } from 'react-router-dom';
import { HeaderSearchItem } from '../headerSearchItem/HeaderSearchItem';

interface HeaderSearchProps {
    className?:string
}

export const HeaderSearch:React.FC<HeaderSearchProps> = ({className}) => {
    const container = `${styles.container} ${className || ''}`.trim();

    const containerRef = useRef<HTMLDivElement>(null);
    const debounceTimeout = useRef<null | number>(null);
    const { setProducts, setSearchTerm, setTotalPageCount, setCategory, setPage, searchTerm, products } = useSearchProductsStore();
    const [isLoading, setLoading] = useState(false);
    const navigator = useNavigate();
    const [notFound, setNotFound] = useState<boolean>(false);
    
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

    const handleKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
        if(e.key === 'Enter'){
            setPage(1);
            goToSearchPage();
        }
    }
    
    const goToSearchPage = () => {
        navigator('/Search');
    }

    const fetchData = async (query:string, category:string = '') => {
        setLoading(true);
        const isAdmin = authStore.isAdmin;
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
        <div className={styles.searchHeader}>
            <button className={styles.searchSvgContainer} onClick={goToSearchPage}>
                <SearchSvg />
            </button>
            <input
                className={styles.searchInput}
                placeholder="Введите артикул или название запчасти"
                onChange={handleInputChange}
                onFocus={handleFocus}
                onKeyDown={handleKeyDown}
            />
            <div className={styles.loadingContainer}>
                {isLoading && <img src="/loading.gif" className={styles.loading}/>}
            </div>
        </div>

        {inFocus && searchTerm !== '' && products.length > 0 && (
            <div className={styles.autoCompleteSearch}>
                <div className={styles.resultsContainer}>
                    {products?.map((product) => (
                        <HeaderSearchItem key={product.id} product={product} />
                    ))}
                </div>
                    
                <div className={styles.autoCompleteFooter}>
                    {notFound && (
                        <div className={styles.redColor}>
                            Ничего не найдено
                        </div>
                    )}
                </div>
            </div>
        )}
    </div>
};