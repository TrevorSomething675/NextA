import GetDetailsResponse from '../../models/details/GetDetailsResponse';
import GetDetailsRequest from '../../models/details/GetDetailsRequest';
import DetailsFilter from '../../models/details/DetailsFilter';
import DetailsService from '../../services/DetailsService';
import React, { useEffect, useState } from 'react';
import DetailItem from '../detailItem/detailItem';
import styles from './details.module.css';
import Pagging from '../pagging/pagging';

const Details:React.FC<{detailsFilter?:DetailsFilter}> = ({detailsFilter = {pageNumber: 1, pageSize: 16}}) => {
    const handlePageNumberChange = (newPageNumber:number = 1) => {
        fetchData(newPageNumber);
    };
    
    const [response, setResponse] = useState<GetDetailsResponse>({} as GetDetailsResponse);
    const fetchData = async (pageNumber:number) => {
        detailsFilter.pageNumber = pageNumber;
        const request:GetDetailsRequest = {
            filter: detailsFilter
        }
        console.log(request);
        const result = await DetailsService.GetDetails(request);
        if(result.statusCode == 200){
            setResponse(result.value)
        }
    };

    useEffect(() => {
        fetchData(detailsFilter.pageNumber);
    }, []);

    return <div className={styles.container}>
        <table className={styles.table}>
            <thead className={styles.thead}>
                <tr className={styles.tr}>
                    <th>Название</th>
                    <th>Артикул</th>
                    <th>Описание</th>
                    <th>Статус</th>
                    <th>Доставка в ПВЗ</th>
                    <th>Кол-во</th>
                    <th>Стоимость</th>
                    <th></th>
                </tr>
            </thead>
            <tbody className={styles.tbody}>
                {(response?.pagedDetails?.items !== undefined) && (response.pagedDetails.items.map((detail) =>
                    <DetailItem key={detail.id} detail={detail} />
                ))}
            </tbody>
        </table>
        {(response?.pagedDetails?.items !== undefined) && <Pagging pageCount={response?.pagedDetails.pageCount} onPageNumberChange={handlePageNumberChange} />}
    </div>
}
export default Details;