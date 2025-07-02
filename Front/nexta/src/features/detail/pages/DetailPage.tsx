import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import DetailsService from "../../../services/DetailsService";
import styles from './DetailPage.module.css';
import DetailHeader from "../components/DetailHeader/detailHeader";
import DetailBody from "../components/DetailBody/detailBody";
import DetailFooter from "../components/DetailFooter/detailFooter";
import { Detail } from "../../../shared/entities/Detail";

const DetailPage = () => {
    const {id} = useParams();
    const [detail, setDetail] = useState({} as Detail)
    
    useEffect(() => {
        const fetch = async() =>{
            if(id !== undefined){
                const response = await DetailsService.GetDetail(id);
                setDetail(response.detail);
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