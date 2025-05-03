import React, { useEffect, useState } from 'react';
import styles from './details.module.css';
import GetDetailsRequest from '../../models/details/GetDetailsRequest';
import DetailsFilter from '../../models/details/DetailsFilter';
import DetailsService from '../../services/DetailsService';
import Detail from '../../models/Detail';
import DetailItem from '../detailItem/detailItem';

const Details:React.FC<{pageNumber?:number}> = ({pageNumber = 1}) => {

    const [details, setDetails] = useState<Detail[]>([]);

    useEffect(() => {
        const fetchData = async () => {
            const filter:DetailsFilter = {
                pageNumber: pageNumber
            }
            const request:GetDetailsRequest = {
                filter: filter
            }
            const result = await DetailsService.GetDetails(request);
            if(result.statusCode == 200){
                setDetails(result.value.pagedDetails.items)
            }
        };
        fetchData();
    }, []);

    return <ul className={styles.container}>
        {(details !== undefined) && (details.map((detail) => 
            <DetailItem key={detail.id} detail={detail}/>
        ))}
    </ul>
}
export default Details;