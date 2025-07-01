import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import styles from './AdminDetail.module.css';
import { GetAdminDetailResponse } from "../../models/GetAdminDetail";
import AdminDetailHeader from "../../components/AdminDetail/AdminDetailHeader/AdminDetailHeader";
import AdminDetailBody from "../../components/AdminDetail/AdminDetailBody/AdminDetailBody";
import AdminDetailFooter from "../../components/AdminDetail/AdminDetailFooter/AdminDetailFooter";
import AdminService from "../../../../services/AdminService";

const AdminDetail = () => {
    const {id} = useParams();
    const [response, setDetail] = useState({} as GetAdminDetailResponse)
    
    useEffect(() => {
        const fetch = async() =>{
            if(id !== undefined){
                const response = await AdminService.GetAdminDetail(id);
                setDetail(response);
            } 
        }
        fetch();
    }, [id]);

    
    return <div className={styles.container}>
        <div className={styles.detailContainer}>
            <AdminDetailHeader detail={response} />
            <AdminDetailBody detail={response} />
            <AdminDetailFooter detail={response} />
        </div>
    </div>
}

export default AdminDetail;