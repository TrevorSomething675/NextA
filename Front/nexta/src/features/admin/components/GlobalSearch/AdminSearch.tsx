import { useEffect, useRef, useState } from "react";
import { GetAdminProductsRequest, GetAdminProductsResponse } from "../../models/AdminProduct/GetAdminProducts";
import AdminService from "../../../../services/AdminService";
import styles from './AdminSearch.module.css';
import SearchSvg from "../../svgs/SearchSvg/SearchSvg";

interface GlobalSearchProps {
    onResponseChange: (response: GetAdminProductsResponse, searchTerm:string) => void;
    className?:string;
    onFetchReady?: (fetchData: (query:string, page:number) => void) => void;
}

export const AdminGlobalSearch: React.FC<GlobalSearchProps> = ({ 
        onResponseChange,
        className,
        onFetchReady
    }) => {
    const [isLoading, setLoading] = useState(false);
    const debounceTimeout = useRef<null | number>(null);
    const [response, setResponse] = useState<GetAdminProductsResponse>({} as GetAdminProductsResponse);

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

    const fetchData = async (query: string, page: number = 1) => {
        const request: GetAdminProductsRequest = {
            searchTerm: query ?? '',
            pageNumber: page ?? 1
        };
        try {
            const response = await AdminService.GetAdminProducts(request);
            if(response.success && response.status === 200){
                const newResponse = response.data;
                setResponse(newResponse);
                onResponseChange(newResponse, query);
            }
        } catch (error) {
            console.error('Ошибка при получении данных:', error);
        }
        setLoading(false);
    };

    useEffect(() => {
        if(onFetchReady){
            onFetchReady(fetchData);
        }
    }, []);

    useEffect(() => {
        fetchData('');
    }, []);

    return (
        <div className={`${styles.container} ${className || ''}`}>
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