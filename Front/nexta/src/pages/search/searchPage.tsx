import GlobalSearch from '../../components/search/globalSearch/globalSearch';
import styles from './searchPage.module.css';
import { useState } from 'react';
import GetDetailsResponse from '../../models/details/GetDetailsResponse';
import DetailItem from '../../components/detailItem/detailItem';

const SearchPage = () => {
    const [detailsResponse, setDetailsResponse] = useState({} as GetDetailsResponse);

    const handleResponseChange = (response:GetDetailsResponse) => {
        setDetailsResponse(response);
    }

    return <div className={styles.container}>
        <h2 className={styles.h2}>Глобальный поиск</h2>
        <div className={styles.header}>
            <GlobalSearch onResponseChange={handleResponseChange}/>
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
            {(detailsResponse?.pagedDetails?.items !== undefined) && (detailsResponse.pagedDetails.items.map((detail) =>
                <DetailItem key={detail.id} detail={detail} />
            ))}
            </tbody>
        </table>
    </div>
}

export default SearchPage;