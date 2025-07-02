import { useEffect, useRef, useState } from "react";
import { GetAdminDetailsFilter, GetAdminDetailsRequest, GetAdminDetailsResponse } from "../../models/GetAdminDetails";
import AdminService from "../../../../services/AdminService";
import SearchSvg from "../../../details/svgs/SearchSvg/SearchSvg";
import styles from './AdminSearch.module.css';

interface GlobalSearchProps {
    onResponseChange: (response: GetAdminDetailsResponse) => void;
}

export const AdminGlobalSearch: React.FC<GlobalSearchProps> = ({ onResponseChange }) => {
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

    const fetchData = async (query: string) => {
        const filter: GetAdminDetailsFilter = {
            pageNumber: 1,
            pageSize: 16,
            searchTerm: query,
            withHidden: true
        };
        const request: GetAdminDetailsRequest = {
            filter
        };
        try {
            const response = await AdminService.GetAdminDetails(request);
            const newResponse = response;
            console.warn(newResponse);
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

export default AdminGlobalSearch;