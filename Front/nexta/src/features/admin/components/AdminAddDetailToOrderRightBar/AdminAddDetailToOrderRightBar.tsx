import { useState } from 'react';
import { GetAdminDetailsResponse } from '../../models/GetAdminDetails';
import AdminGlobalSearch from '../GlobalSearch/AdminSearch';
import styles from './AdminAddDetailToOrderRightBar.module.css';
import { AddDetailToOrderItem } from './AddDetailToOrderItem/AddDetailToOrderItem';
import Pagging from '../../../../shared/components/Pagging/Pagging';
import Button from '../../../../shared/components/Button/Button';
import { Detail } from '../../../../shared/entities/Detail';

interface AdminAddDetailToOrderRightBarProps {
    orderId: string;
    onClose: () => void;
    onAddToOrder: (detail: Detail, count: number) => void;
}

export const AdminAddDetailToOrderRightBar: React.FC<AdminAddDetailToOrderRightBarProps> = ({ orderId, onClose, onAddToOrder }) => {

    const [detailsResponse, setDetailsResponse] = useState({} as GetAdminDetailsResponse);
    const [currentPage, setCurrentPage] = useState(1);
    const [searchTerm, setSearchTerm] = useState('');
    const [fetchData, setFetchData] = useState<(query: string, page: number) => void>();

    const handleAddPrevDetail = (detail:Detail, count:number) =>{
        onAddToOrder(detail, count);
    }

    const handleResponseChange = (response: GetAdminDetailsResponse, term: string) => {
        setDetailsResponse(response);
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
            {(detailsResponse?.data?.items !== undefined) && 
                detailsResponse.data.items.map((detail) =>
                <AddDetailToOrderItem 
                    onAddToOrder={handleAddPrevDetail}
                    key={detail.id} 
                    detail={detail} 
                />
                )
            }
            </tbody>
        </table>
        {(detailsResponse?.data?.items !== undefined) && <Pagging 
            pageCount={detailsResponse.data.pageCount} 
            onPageNumberChange={handleChangePage} 
        />}
    </div>
}