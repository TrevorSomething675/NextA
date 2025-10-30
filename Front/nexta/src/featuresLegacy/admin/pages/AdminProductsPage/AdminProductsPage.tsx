import { CreateAdminProduct } from "../../components/adminProducts/CreateAdminProduct/CreateAdminProduct";
import styles from './AdminProductsPage.module.css';
import { SearchProductsContainer } from "../../../product/components/SearchProductsContainer/SearchProductsContainer";
import Pagging from "../../../../shared/ui/pagging/Pagging";
import { useEffect, useState } from "react";
import { GetProductsResponse } from "../../../../http/models/product/GetProducts";
import { useSearchProductsStore } from "../../../../shared/stores/searchProduct/searchProductsStore";
import { SearchProducts } from "../../../../widgets/ui/search/ui/headerSearch/searchProduct/SearchProducts";
import authStore from "../../../../shared/stores/auth/authStore";
import { ProductApi } from "../../../../shared/http/product/productApi";

export const AdminProductsPage = () => {
    const [page, setPage] = useState<number>(1);
    const [pageSize] = useState<number>(12);
    const [productsResponse, setProductsResponse] = useState<GetProductsResponse>({} as GetProductsResponse);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [legacySearchTerm, setLegacySearchTerm] = useState<string>('');

    const { setProducts, setSearchTerm, setTotalPageCount, products, searchTerm, totalPageCount } = useSearchProductsStore();

    const fetchProducts = async (searchTerm: string, pageNumber: number) => {

        if(legacySearchTerm != searchTerm){
            setLegacySearchTerm(searchTerm);
            setPage(1);
            pageNumber = 1;
        }

        setIsLoading(true);
        try {
            const isAdmin:boolean = authStore.isAdmin;
            const response = await ProductApi.GetAll(searchTerm, '', pageSize, pageNumber, isAdmin);

            if (response.success && response.status === 200) {
                setSearchTerm(searchTerm);
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
        fetchProducts(searchTerm, page);
    }, [searchTerm, page]);

    const handlePageChange = (pageNumber: number) => {
        fetchProducts(searchTerm, pageNumber);
    };

    return <div className={styles.container}>
        <h2 className={styles.createProduct}>Создать товар</h2>
        <CreateAdminProduct />
        <h2 className={styles.h2}>Глобальный поиск</h2>
        <div className={styles.header}>
            <SearchProducts />
        </div>
        <SearchProductsContainer products={products} />
        <Pagging
            pageCount={totalPageCount || 0}
            onPageNumberChange={handlePageChange}
        />
    </div>
}