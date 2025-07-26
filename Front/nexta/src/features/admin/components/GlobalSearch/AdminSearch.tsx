import { useEffect, useRef, useState } from "react";
import { GetAdminDetailsFilter, GetAdminDetailsRequest, GetAdminDetailsResponse } from "../../models/GetAdminDetails";
import AdminService from "../../../../services/AdminService";
import SearchSvg from "../../../details/svgs/SearchSvg/SearchSvg";
import styles from './AdminSearch.module.css';

interface GlobalSearchProps {
    onResponseChange: (response: GetAdminDetailsResponse, searchTerm:string) => void;
    className?:string;
    onFetchReady?: (fetchData: (query:string, page:number) => void) => void;
}

const AdminGlobalSearch: React.FC<GlobalSearchProps> = ({ 
        onResponseChange,
        className,
        onFetchReady
    }) => {
    const [isLoading, setLoading] = useState(false);
    const debounceTimeout = useRef<null | number>(null);
    const [response, setResponse] = useState<GetAdminDetailsResponse>({} as GetAdminDetailsResponse);

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
        const filter: GetAdminDetailsFilter = {
            pageNumber: page,
            pageSize: 8,
            searchTerm: query,
            withHidden: true
        };
        const request: GetAdminDetailsRequest = {
            filter
        };
        try {
            const response = await AdminService.GetAdminDetails(request);
            const newResponse = response;
            setResponse(newResponse);
            onResponseChange(newResponse, query);
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

export default AdminGlobalSearch;