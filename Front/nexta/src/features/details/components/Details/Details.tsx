import { useEffect, useState } from 'react';
import styles from './Details.module.css';
import DetailsService from '../../../../services/DetailsService';
import { GetDetailsFilter, GetDetailsRequest, GetDetailsResponse } from '../../models/GetDetails';
import DetailItem from '../DetailItem/DetailItem';
import Pagging from '../../../../shared/components/Pagging/Pagging';

const Details:React.FC<{detailsFilter?:GetDetailsFilter}> = ({detailsFilter = {pageNumber: 1, pageSize: 16}}) => {
    const handlePageNumberChange = (newPageNumber:number = 1) => {
        fetchData(newPageNumber);
    };
    
    const [response, setResponse] = useState<GetDetailsResponse>({} as GetDetailsResponse);
    const fetchData = async (pageNumber:number) => {
        detailsFilter.pageNumber = pageNumber;
        const request:GetDetailsRequest = {
            filter: detailsFilter
        }
        const result = await DetailsService.GetDetails(request);
        if(result){
            setResponse(result);
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
                {(response?.details?.items !== undefined) && (response.details.items.map((detail) =>
                    <DetailItem key={detail.id} detail={detail} />
                ))}
            </tbody>
        </table>
        {(response?.details?.items !== undefined) && <Pagging pageCount={response?.details.pageCount} onPageNumberChange={handlePageNumberChange} />}
    </div>
}
export default Details;