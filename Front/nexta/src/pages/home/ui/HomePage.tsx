import { useEffect, useState } from 'react';
import styles from './HomePage.module.css';
import { ProductsContainer } from '../../../widgets/ui/products-container/ProductsContainer';
import { ProductApi } from '../../../shared/http/product/productApi';
import { Product } from '../../../entities/product/product';
import { Button } from '../../../shared/ui';

export const HomePage = () => {
    const [pageProducts, setProducts] = useState<Product[]>([]);
    const [pageNumber, setPageNumber] = useState(1);
    const [totalPageCount, setTotalPageCount] = useState(-1);

    useEffect(() => {
        fetchData(pageNumber);
    }, []);

    const handleClickMoreProducts = () => {
        const newPageCount = pageNumber + 1;
        setPageNumber(newPageCount);
        
        fetchData(newPageCount);
    }

    const fetchData = async(pageNumber:number = 1) => {
        const response = await ProductApi.GetAll('', '', 12, pageNumber);
        if(response.success && response.status === 200){
            const products = response.data.items.map(prod => ({
                ...prod
            } as Product));
            setTotalPageCount(response.data.pageCount);
            const resultProducts = pageProducts.concat(products);
            setProducts(resultProducts);
        }
    }

    return <div className={styles.container}>
        <h2 className={styles.h2}>Новости</h2>
        <h2 className={styles.h2}>Список товаров</h2>
        <div className={styles.productsContainer}>
            <ProductsContainer products={pageProducts}/>
        </div>
        <div className={styles.buttonContainer}>
            {
                (pageNumber !== totalPageCount) &&
                <Button className={styles.button} onClick={handleClickMoreProducts}>Загрузить ещё</Button>
            }
        </div>
    </div>
}