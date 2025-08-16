import { useRef, useState } from 'react';
import styles from './AdminDetailsPage.module.css';
import { GetAdminDetailsResponse } from '../../models/GetAdminDetails';
import AdminDetailItem from '../../components/AdminDetailItem/AdminDetailItem';
import AdminGlobalSearch from '../../components/GlobalSearch/AdminSearch';
import Pagging from '../../../../shared/components/Pagging/Pagging';
import { CreateAdminDetail } from '../../components/CreateAdminDetail/CreateAdminDetail';

const AdminDetailsPage = () => {
    const [detailsResponse, setDetailsResponse] = useState({} as GetAdminDetailsResponse);
    const [searchTerm, setSearchTerm] = useState('');
    const fetchDataRef = useRef<(query: string, page: number) => void>(null);

    const handleResponseChange = (response: GetAdminDetailsResponse, term: string) => {
        setDetailsResponse(response);
        setSearchTerm(term);
    }

    const handleFetchReady = (fetchData: (query: string, page: number) => void) => {
        fetchDataRef.current = fetchData;
    }

    const handleChangePage = (page: number) => {
        if (fetchDataRef.current) {
            fetchDataRef.current(searchTerm, page);
        }
    }

    return <div className={styles.container}>
        <h2 className={styles.createDetail}>Создать деталь</h2>
        <CreateAdminDetail />
        <h2 className={styles.h2}>Глобальный поиск</h2>
        <div className={styles.header}>
            <AdminGlobalSearch 
                onResponseChange={handleResponseChange} 
                onFetchReady={handleFetchReady}
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
            {(detailsResponse?.data?.items !== undefined) && (detailsResponse.data.items.map((detail) =>
                <AdminDetailItem key={detail.id} detail={detail} />
            ))}
            </tbody>
        </table>
        <Pagging 
            pageCount={detailsResponse?.data?.pageCount} 
            onPageNumberChange={handleChangePage}
        />
    </div>
}

export default AdminDetailsPage;