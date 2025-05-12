import React, { useEffect, useState } from 'react';
import styles from './warehouseDetails.module.css';
import GetDetailsRequest from '../../models/details/GetDetailsRequest';
import DetailsFilter from '../../models/details/DetailsFilter';
import DetailItem from '../detailItem/detailItem';
import Pagging from '../pagging/pagging';
import WarehouseService from '../../services/WarehouseService';
import GetWarehouseResponse from '../../models/warehouse/GetWarehouseResponse';

const WarehouseDetails:React.FC<{pageNumber?:number}> = () => {
    const handlePageNumberChange = (pageNumber:number = 1) => {
        fetchData(pageNumber);
    };

    const [response, setResponse] = useState<GetWarehouseResponse>({} as GetWarehouseResponse);
    const fetchData = async (pageNumber:number) => {
        const filter:DetailsFilter = {
            pageNumber: pageNumber
        }
        const request:GetDetailsRequest = {
            filter: filter
        }
        const result = await WarehouseService.GetDetails(request);
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
                    <th>Кол-воб, шт</th>
                    <th>Стоимость, ₽</th>
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
export default WarehouseDetails;