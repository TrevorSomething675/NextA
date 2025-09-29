import { useEffect, useState } from "react";
import styles from './SearchPage.module.css';
import { SearchProductsContainer } from "../../../product/components/SearchProductsContainer/SearchProductsContainer";
import ProductsService from "../../../../services/ProductService";
import { useSearchProductsStore } from "../../../../stores/SearchProductsStore/searchProductsStore";
import { SearchSidebar } from "../../components/SearchSidebar/SearchSidebar";
import Button from "../../../../shared/components/Button/Button";

const SearchPage = () => {
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
    }, [searchTerm, category, debouncedPriceFilters]);

    const handleScrollDown = () => {
        const newPage = page + 1;
        handlePageChange(newPage);
        setPage(newPage);
    }

    const handlePageChange = async(pageNumber: number) => {
        const response = await ProductsService.Get(
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
            setTotalPageCount(response.data.data.pageCount);
            setProducts([...products, ...response.data.data.items]);
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
                        content='Загрузить ещё'
                        className={styles.scrollBtn}
                    />}
                </>
            )}
        </div>
    );
};

export default SearchPage;