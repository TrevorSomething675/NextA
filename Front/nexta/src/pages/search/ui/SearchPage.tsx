import { useEffect, useState } from "react";
import styles from './SearchPage.module.css';
import { SearchProductsContainer } from "../../../widgets/ui/search/ui/searchProductsContainer/SearchProductsContainer";
import { useSearchProductsStore } from "../../../shared/stores/searchProduct/searchProductsStore";
import { SearchSidebar } from "../../../widgets/ui/search/ui/searchSidebar/SearchSidebar";
import { Button } from "../../../shared/ui";
import { ProductApi } from "../../../shared/http/product/productApi";

export const SearchPage = () => {
    const [isLoading, setIsLoading] = useState<boolean>(false);

    const { 
        products,
        searchTerm,
        category,
        priceFilters,
        page,
        isPriceRangeImmediate,
        totalPageCount,
        setPage,
        setProducts,
        setSearchTerm,
        setTotalPageCount,
        setCategory,
        clearImmediatePriceUpdate
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
            const response = await ProductApi.GetAll(
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
                setTotalPageCount(response.data.pageCount);
                setProducts(response.data.items);
            }
        } catch (error) {
            console.error('Ошибка:', error);
        } finally {
            setIsLoading(false);
        }
    };

    useEffect(() => {
        fetchProducts();
    }, [searchTerm, category, debouncedPriceFilters]);

    const handleScrollDown = () => {
        const newPage = page + 1;
        handlePageChange(newPage);
        setPage(newPage);
    }

    const handlePageChange = async(pageNumber: number) => {
        const response = await ProductApi.GetAll(
            searchTerm,
            category,
            9,
            pageNumber,
            false,
            debouncedPriceFilters.min,
            debouncedPriceFilters.max
        );
        if (response.success && response.status === 200) {
            setCategory(category);
            setSearchTerm(searchTerm);
            setTotalPageCount(response.data.pageCount);
            setProducts([...products, ...response.data.items]);
        }
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
                    {page !== totalPageCount && <Button 
                        onClick={handleScrollDown}
                        className={styles.scrollBtn}
                    >
                        Загрузить ещё
                    </Button>}
                </>
            )}
        </div>
    );
};