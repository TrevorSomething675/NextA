import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import DetailsService from "../../../services/DetailsService";
import { Detail } from "../../../shared/entities/Detail";
import styles from './DetailPage.module.css';
import DetailHeader from "../components/DetailHeader/DetailHeader";
import DetailBody from "../components/DetailBody/DetailBody";
import DetailFooter from "../components/DetailFooter/DetailFooter";

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
            <DetailFooter detail={detail} />
        </div>
    </div>
}

export default DetailPage;