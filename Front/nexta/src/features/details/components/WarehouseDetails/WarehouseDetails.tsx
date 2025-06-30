import { useEffect, useState } from 'react';
import styles from './WarehouseDetails.module.css';
import { GetDetailsFilter, GetDetailsRequest } from '../../models/GetDetails';
import WarehouseService from '../../../../services/WarehouseService';
import DetailItem from '../DetailItem/DetailItem';
import { GetWarehouseResponse } from '../../models/GetWarehouse';
import Pagging from '../../../../shared/components/Pagging/Pagging';

const WarehouseDetails:React.FC<{pageNumber?:number}> = () => {
    const handlePageNumberChange = (pageNumber:number = 1) => {
        fetchData(pageNumber);
    };

    const [response, setResponse] = useState<GetWarehouseResponse>({} as GetWarehouseResponse);
    const fetchData = async (pageNumber:number) => {
        const filter:GetDetailsFilter = {
            pageSize: 8,
            pageNumber: pageNumber
        }
        const request:GetDetailsRequest = {
            filter: filter
        }
        const response = await WarehouseService.GetDetails(request);
        setResponse(response);
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
                    <th>Кол-во, шт</th>
                    <th>Стоимость, ₽</th>
                    <th></th>
                </tr>
            </thead>
            <tbody className={styles.tbody}>
                {(response?.data?.items !== undefined) && (response.data.items.map((detail) =>
                    <DetailItem key={detail.id} detail={detail} />
                ))}
            </tbody>
        </table>
        {(response?.data?.items !== undefined) && <Pagging pageCount={response?.data.pageCount} onPageNumberChange={handlePageNumberChange} />}
    </div>
}

export default WarehouseDetails;