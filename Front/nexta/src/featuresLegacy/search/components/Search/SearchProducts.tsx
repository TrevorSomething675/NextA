import { useRef, useState } from "react";
import styles from './SearchProducts.module.css';
import { useSearchProductsStore } from "../../../../shared/stores/searchProduct/searchProductsStore";
import SearchSvg from "../../../../widgets/ui/svg/SearchSvg/SearchSvg";
import authStore from "../../../../shared/stores/auth/authStore";
import { ProductApi } from "../../../../entities/product/api/productApi";

interface Props {
    className?:string
}

export const SearchProducts:React.FC<Props> = ({className}) => {
    const container = `${styles.container} ${className || ''}`.trim();

    const debounceTimeout = useRef<null | number>(null);
    const { setSearchTerm } = useSearchProductsStore();
    const [isLoading, setLoading] = useState(false);

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

    const fetchData = async (query:string) => {
        setLoading(true);
        const isAdmin = authStore.isAdmin;
        const response = await ProductApi.GetAll(query, '', 8, 1, isAdmin);
        if(response.success && response.status === 200){
            setSearchTerm(query);
        }
        setLoading(false);
    };

    return <div className={container}>
        <div className={styles.searchHeader}>
            <div className={styles.searchSvgContainer}>
                <SearchSvg />
            </div>
            <input
                className={styles.searchInput}
                placeholder="Введите артикул или название запчасти"
                onChange={handleInputChange}
            />
            <div className={styles.loadingContainer}>
                {isLoading && <img src="/loading.gif" className={styles.loading}/>}
            </div>
        </div>
    </div>
};