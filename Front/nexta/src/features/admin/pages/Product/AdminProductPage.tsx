import { useParams } from 'react-router-dom';
import AdminProductHeader from '../../components/adminProducts/AdminProduct/AdminProductHeader/AdminProductHeader';
import styles from './AdminProductPage.module.css';
import { useCallback, useEffect, useState } from 'react';
import { AdminProduct } from '../../models/AdminProduct';
import AdminService from '../../../../services/AdminService';
import { AdminProductBody } from '../../components/adminProducts/AdminProduct/AdminProductBody/AdminProductBody';

export const AdminProductPage = () => {
    const {id} = useParams();
    const [product, setProduct] = useState({} as AdminProduct)

    const fetchData = useCallback(async () => {
        if (id) {
            const response = await AdminService.GetAdminProduct(id);
            if(response.success && response.status === 200){
                setProduct(response.data.product);
            }
        }
    }, [id]);

    useEffect(() => {
        fetchData();
    }, [fetchData]);

    return <div className={styles.container}>
        <div className={styles.productContainer}>
            <AdminProductHeader product={product} />
            <AdminProductBody product={product} onUpdate={fetchData} />
        </div>
    </div>
}