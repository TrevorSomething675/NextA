import { useEffect, useRef, useState } from 'react';
import styles from './HeaderSearch.module.css';
import { useNavigate } from 'react-router-dom';
import SearchSvg from '../../../../shared/svgs/SearchSvg/SearchSvg';
import ProductsService from '../../../../services/ProductService';
import { GetProductsResponse } from '../../../../http/models/product/GetProducts';
import { SearchItem } from '../HeaderSearchItem/HeaderSearchItem';
import { useSearchProductsStore } from '../../../../stores/SearchProductsStore/searchProductsStore';
import authStore from '../../../../stores/AuthStore/authStore';

interface Props {
    className?:string
}

export const HeaderSearch:React.FC<Props> = ({className}) => {
    const container = `${styles.container} ${className || ''}`.trim();

    const containerRef = useRef<HTMLDivElement>(null);
    const debounceTimeout = useRef<null | number>(null);
    const { setProducts, setSearchTerm, setTotalPageCount, setCategory, setPage, searchTerm, products } = useSearchProductsStore();
    const [response, setResponse] = useState<GetProductsResponse>({} as GetProductsResponse);
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
        const response = await ProductsService.Get(query, category, 8, 1, isAdmin);
        if(response.success && response.status === 200){
            setCategory('');
            setSearchTerm(query);
            setResponse(response.data);
            setProducts(response.data.data.items);
            setTotalPageCount(response.data.data.pageCount);

            if(response.data.data.items.length === 0){
                setNotFound(true);
            } else{
                setNotFound(false);
            }
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
                        <SearchItem key={product.id} product={product} />
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