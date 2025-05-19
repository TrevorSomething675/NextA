import GetDetailsResponse from '../../models/details/GetDetailsResponse';
import GetDetailsRequest from '../../models/details/GetDetailsRequest';
import DetailsFilter from '../../models/details/DetailsFilter';
import DetailsService from '../../services/DetailsService';
import React, { useEffect, useState } from 'react';
import DetailItem from '../detailItem/detailItem';
import styles from './details.module.css';
import Pagging from '../pagging/pagging';

const Details:React.FC = () => {
    const handlePageNumberChange = (pageNumber:number = 1) => {
        fetchData(pageNumber);
    };
    
    const [response, setResponse] = useState<GetDetailsResponse>({} as GetDetailsResponse);
    const fetchData = async (pageNumber:number) => {
        const filter:DetailsFilter = {
            pageNumber: pageNumber
        }
        const request:GetDetailsRequest = {
            filter: filter
        }
        const result = await DetailsService.GetDetails(request);
        if(result.statusCode == 200){
            setResponse(result.value)
        }
    };

    useEffect(() => {
        fetchData(1);
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