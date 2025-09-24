import { useEffect, useState } from "react";
import styles from './SearchPage.module.css';
import { SearchProductsContainer } from "../../../product/components/SearchProductsContainer/SearchProductsContainer";
import ProductsService from "../../../../services/ProductService";
import Pagging from "../../../../shared/components/Pagging/Pagging";
import { useSearchProductsStore } from "../../../../stores/SearchProductsStore/searchProductsStore";
import { SearchSidebar } from "../../components/SearchSidebar/SearchSidebar";

const SearchPage = () => {
    const [isLoading, setIsLoading] = useState<boolean>(false);

    const { 
        products,
        searchTerm,
        category,
        totalPageCount,
        priceFilters,
        page,
        isPriceRangeImmediate,
        setProducts,
        setSearchTerm,
        setTotalPageCount,
        setCategory,
        setPage,
        clearImmediatePriceUpdate,
    } = useSearchProductsStore();

    const [debouncedPriceFilters, setDebouncedPriceFilters] = useState(priceFilters);

    useEffect(() => {
        if (isPriceRangeImmediate) {
            setDebouncedPriceFilters(priceFilters);
            clearImmediatePriceUpdate();
            return;
        }

        const timeoutId = setTimeout(() => {
            setDebouncedPriceFilters(priceFilters);
        }, 1000);

        return () => clearTimeout(timeoutId);
    }, [priceFilters, isPriceRangeImmediate]);

    const fetchProducts = async () => {
        setIsLoading(true);
        try {
            const response = await ProductsService.Get(
                searchTerm,
                category,
                9,
                page,
                false,
                debouncedPriceFilters.min,
                debouncedPriceFilters.max
            );
            if (response.success && response.status === 200) {
                setCategory(category);
                setSearchTerm(searchTerm);
                setTotalPageCount(response.data.data.pageCount);
                setProducts(response.data.data.items);
            }
        } catch (error) {
            console.error('Ошибка:', error);
        } finally {
            setIsLoading(false);
        }
    };

    useEffect(() => {
        fetchProducts();
    }, [searchTerm, category, page, debouncedPriceFilters]);

    const handlePageChange = (pageNumber: number) => {
        setPage(pageNumber);
    };
    
    return (
        <div className={styles.container}>
            <h2 className={styles.h2}>Глобальный поиск</h2>
            
            {isLoading ? (
                <div>Загрузка...</div>
            ) : (
                <>
                    <div className={styles.productsContainer}>
                        <div className={styles.sideBarContainer}>
                            <SearchSidebar />
                        </div>
                        
                        <SearchProductsContainer products={products} />
                    </div>
                    <Pagging
                        pageCount={totalPageCount || 0}
                        onPageNumberChange={handlePageChange}
                    />
                </>
            )}
        </div>
    );
};

export default SearchPage;