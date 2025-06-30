import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import DetailsService from "../../../services/DetailsService";
import styles from './DetailPage.module.css';
import DetailHeader from "../components/DetailHeader/detailHeader";
import DetailBody from "../components/DetailBody/detailBody";
import DetailFooter from "../components/DetailFooter/detailFooter";
import { GetDetailResponse } from "../models/GetDetail";

const DetailPage = () => {
    const {id} = useParams();
    const [response, setDetail] = useState({} as GetDetailResponse)
    
    useEffect(() => {
        const fetch = async() =>{
            if(id !== undefined){
                const response = await DetailsService.GetDetail(id);
                setDetail(response);
            }
        }
        fetch();
    }, [id]);

    
    return <div className={styles.container}>
        <div className={styles.detailContainer}>
            <DetailHeader detail={response} />
            <DetailBody detail={response} />
            <DetailFooter detail={response} />
        </div>
    </div>
}

export default DetailPage;