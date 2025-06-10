import styles from './detailPage.module.css';
import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import DetailsService from '../../services/DetailsService';
import Detail from '../../models/Detail';
import DetailHeader from '../../components/detail/detailHeader/detailHeader';
import DetailBody from '../../components/detail/detailBody/detailBody';

const DetailPage = () => {
    const {id} = useParams();
    const [detail, setDetail] = useState({} as Detail)
    useEffect(() => {
        const fetch = async() =>{
            if(id !== undefined){
                const detail = await DetailsService.GetDetail(id);
                setDetail(detail?.detail);
            }
        }
        fetch();
    }, [id]);

    return <div className={styles.container}>
        <div className={styles.detailContainer}>
            <DetailHeader detail={detail} />
            <DetailBody detail={detail} />
        </div>
    </div>
}

export default DetailPage;