import { useEffect, useState } from 'react';
import styles from './ProductsContainer.module.css';
import { GetWarehouseResponse } from '../../../details/models/GetWarehouse';
import { GetDetailsFilter, GetDetailsRequest } from '../../../details/models/GetDetails';
import WarehouseService from '../../../../services/WarehouseService';
import { ProductCard } from '../ProductCard/ProductCard';
import Pagging from '../../../../shared/components/Pagging/Pagging';

export const ProductsContainer:React.FC<{pageNumber?: number}> = () => {
    const handlePageNumberChange = (pageNumber:number = 1) => {
        fetchData(pageNumber);
    };

    const [response, setResponse] = useState<GetWarehouseResponse>({} as GetWarehouseResponse);
    const fetchData = async (pageNumber:number) => {
        const filter:GetDetailsFilter = {
            pageSize: 8,
            pageNumber: pageNumber
        }
        const request:GetDetailsRequest = {
            filter: filter
        }
        const response = await WarehouseService.GetDetails(request);
        setResponse(response);
    };

    useEffect(() => {
        fetchData(1);
    }, []);


    return <div className={styles.container}>
        <div className={styles.products}>
            {(response?.data?.items !== undefined) && (response.data.items.map((detail) => 
                <ProductCard key={detail.id} detail={detail} />
            ))}
        </div>
        <div className={styles.paggingContainer}>
            {(response?.data?.items !== undefined) && <Pagging pageCount={response?.data.pageCount} onPageNumberChange={handlePageNumberChange} />}
        </div>
    </div>
}