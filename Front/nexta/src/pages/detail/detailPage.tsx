import styles from './detailPage.module.css';
import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import DetailsService from '../../services/DetailsService';
import Detail from '../../models/Detail';

const DetailPage = () => {
    const {id} = useParams();
    const [detail, setDetail] = useState({} as Detail)
    useEffect(() => {
        const fetch = async() =>{
            if(id !== undefined){
                const detail = await DetailsService.GetDetail(id);
                setDetail(detail?.value?.detail);
            }
        }
        fetch();
    }, [id]);

    return <div className={styles.container}>
        {detail !== undefined && detail.id}
    </div>
}

export default DetailPage;