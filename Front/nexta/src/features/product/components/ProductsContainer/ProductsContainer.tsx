import { useEffect, useState } from 'react';
import styles from './ProductsContainer.module.css';
import { ProductCard } from '../ProductCard/ProductCard';
import Pagging from '../../../../shared/components/Pagging/Pagging';
import ProductsService from '../../../../services/ProductService';
import { GetProductsResponse } from '../../../../http/models/product/GetProducts';

export const ProductsContainer:React.FC<{pageNumber?: number}> = () => {
    const [response, setResponse] = useState<GetProductsResponse>({} as GetProductsResponse);
    
    const handlePageNumberChange = (pageNumber:number = 1) => {
        fetchData('', pageNumber);
    };

    const fetchData = async (searchTerm:string = '',  pageNumber?:number, pageSize?:number) => {
            const response = await ProductsService.Get(searchTerm, pageSize, pageNumber, false);
            if(response.success && response.status === 200){
                setResponse(response.data);
            }
        }

    useEffect(() => {
        fetchData('');
    }, []);


    return <div className={styles.container}>
        <div className={styles.products}>
            {(response?.data?.items !== undefined) && (response.data.items.map((product) => 
                <ProductCard key={product.id} product={product} />
            ))}
        </div>
        <div className={styles.paggingContainer}>
            {(response?.data?.items !== undefined) && <Pagging pageCount={response?.data.pageCount} onPageNumberChange={handlePageNumberChange} />}
        </div>
    </div>
}