import { useState } from 'react';
import styles from './AdminDetailsPage.module.css';
import { GetAdminDetailsResponse } from '../../models/GetAdminDetails';
import AdminDetailItem from '../../components/AdminDetailItem/AdminDetailItem';
import AdminGlobalSearch from '../../components/GlobalSearch/AdminSearch';

const AdminDetailsPage = () => {
    const [detailsResponse, setDetailsResponse] = useState({} as GetAdminDetailsResponse);

    const handleResponseChange = (response:GetAdminDetailsResponse) => {
        setDetailsResponse(response);
    }

    return <div className={styles.container}>
        <h2 className={styles.h2}>Глобальный поиск</h2>
        <div className={styles.header}>
            <AdminGlobalSearch onResponseChange={handleResponseChange}/>
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
    </div>
}

export default AdminDetailsPage;