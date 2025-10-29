import { useEffect, useState } from 'react';
import styles from './HomePage.module.css';
import { ProductsContainer } from '../../../widgets/ui/products-container/ui/ProductsContainer';
import { Product } from '../../../entities/product/models/product';
import { ProductApi } from '../../../shared/http/product/productApi';

export const HomePage = () => {
    const [pageProducts, setProducts] = useState<Product[]>([]);
    useEffect(() => {
        fetchData(1);
    }, []);

    const fetchData = async(pageNumber:number = 1) => {
        const response = await ProductApi.GetAll('', '', 12, pageNumber);
        if(response.success && response.status === 200){
            const products = response.data.items.map(prod => ({
                ...prod
            } as Product));
            const resultProducts = pageProducts.concat(products);
            setProducts(resultProducts);
        }
    }

    return <div className={styles.container}>
        <h2 className={styles.h2}>Новости</h2>
        <h2 className={styles.h2}>Список товаров</h2>
        <ProductsContainer products={pageProducts}/>
    </div>
}