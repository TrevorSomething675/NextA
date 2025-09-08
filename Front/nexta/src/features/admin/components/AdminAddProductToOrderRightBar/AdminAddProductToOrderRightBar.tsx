import { useState } from 'react';
import styles from './AdminAddProductToOrderRightBar.module.css';
import { AddProductToOrderItem } from './AddDetailToOrderItem/AddProductToOrderItem';
import Pagging from '../../../../shared/components/Pagging/Pagging';
import Button from '../../../../shared/components/Button/Button';
import { AdminGlobalSearch } from '../GlobalSearch/AdminSearch';
import { GetAdminProductsResponse } from '../../models/AdminProduct/GetAdminProducts';
import { AdminProduct } from '../../models/AdminProduct';

interface AdminAddProductToOrderRightBarProps {
    orderId: string;
    onClose?: () => void;
    onAddProduct: (product: AdminProduct, count: number) => void;
}

export const AdminAddProductToOrderRightBar: React.FC<AdminAddProductToOrderRightBarProps> = ({ orderId, onClose, onAddProduct }) => {

    const [productsResponse, setProductsResponse] = useState({} as GetAdminProductsResponse);
    const [currentPage, setCurrentPage] = useState(1);
    const [searchTerm, setSearchTerm] = useState('');
    const [fetchData, setFetchData] = useState<(query: string, page: number) => void>();

    const handleAddPrevProduct = (product:AdminProduct, count:number) => {
        onAddProduct(product, count);
    }

    const handleResponseChange = (response: GetAdminProductsResponse, term: string) => {
        setProductsResponse(response);
        setSearchTerm(term);
        setCurrentPage(1);
    };

    const handleChangePage = (newPage: number) => {
        setCurrentPage(newPage);
        if (fetchData) {
            fetchData(searchTerm, newPage);
        }
    };

    return <div className={styles.container}>
        <div className={styles.headerContainer}>
            <h2 className={styles.h2}>Заказ: {orderId}</h2>
            <Button 
                content='Закрыть' 
                className={styles.closeBtn}
                onClick={onClose}
            />
        </div>
        <div className={styles.searchContainer}>
            <AdminGlobalSearch
                onResponseChange={handleResponseChange}
                onFetchReady={(fetchDataFunc) => setFetchData(() => fetchDataFunc)}
                className={styles.search}
            />
        </div>
        <table className={styles.table}>
            <thead className={styles.thead}>
                <tr className={styles.tr}>
                    <th>Название</th>
                    <th>Артикул</th>
                    <th>Описание</th>
                    <th>Статус</th>
                    <th>Доставка в ПВЗ</th>
                    <th>Кол-во, шт</th>
                    <th>Стоимость, ₽</th>
                    <th></th>
                </tr>
            </thead>
            <tbody className={styles.tbody}>
            {(productsResponse?.data?.items !== undefined) && 
                productsResponse.data.items.map((product) =>
                <AddProductToOrderItem 
                    product={product} 
                    onAddToOrder={handleAddPrevProduct}
                    key={product.id} 
                />
                )
            }
            </tbody>
        </table>
        {(productsResponse?.data?.items !== undefined) && <Pagging 
            pageCount={productsResponse.data.pageCount} 
            onPageNumberChange={handleChangePage} 
        />}
    </div>
}