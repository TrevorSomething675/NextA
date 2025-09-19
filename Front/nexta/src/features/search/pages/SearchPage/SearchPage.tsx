import { useEffect, useState } from "react";
import styles from './SearchPage.module.css';
import { GetProductsResponse } from "../../../../http/models/product/GetProducts";
import { SearchProductsContainer } from "../../../product/components/SearchProductsContainer/SearchProductsContainer";
import ProductsService from "../../../../services/ProductService";
import Pagging from "../../../../shared/components/Pagging/Pagging";
import { useSearchProductsStore } from "../../../../stores/SearchProductsStore/searchProductsStore";

const SearchPage = () => {
    const [page, setPage] = useState<number>(1);
    const [pageSize] = useState<number>(12);
    const [productsResponse, setProductsResponse] = useState<GetProductsResponse>({} as GetProductsResponse);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [legacySearchTerm, setLegacySearchTerm] = useState<string>('');
    const [legacyCategoryTerm, setLegacyCategoryTerm] = useState<string>('');

    const { setProducts, setSearchTerm, setTotalPageCount, setCategory, products, searchTerm, totalPageCount, category } = useSearchProductsStore();

    const fetchProducts = async (searchTerm: string, category:string = '', pageNumber: number) => {

        if(legacySearchTerm != searchTerm || legacyCategoryTerm != category){
            setLegacySearchTerm(searchTerm);
            setLegacyCategoryTerm(category);
            setPage(1);
            pageNumber = 1;
        }

        setIsLoading(true);
        try {
            const response = await ProductsService.Get(searchTerm, category, pageSize, pageNumber);
            console.warn(response);
            console.error(totalPageCount);
            if (response.success && response.status === 200) {
                console.warn()
                setCategory(category);
                setSearchTerm(searchTerm);
                setProductsResponse(response.data);
                setTotalPageCount(response.data.data.pageCount);
                setProducts(response.data.data.items);
            } else {
                setProductsResponse({} as GetProductsResponse);
            }
        } catch (error) {
            console.error('Ошибка при получении данных:', error);
            setProductsResponse({} as GetProductsResponse);
        }
        setIsLoading(false);
    };

    useEffect(() => {
        fetchProducts(searchTerm, category, page);
    }, [searchTerm, category, page]);

    const handlePageChange = (pageNumber: number) => {
        fetchProducts(searchTerm, category, pageNumber);
    };

    return (
        <div className={styles.container}>
            <h2 className={styles.h2}>Глобальный поиск</h2>

            {isLoading ? (
                <div>Загрузка...</div>
            ) : (
                <>
                    <SearchProductsContainer products={products} />
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