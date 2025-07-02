import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import AdminDetailHeader from "../../components/AdminDetail/AdminDetailHeader/AdminDetailHeader";
import AdminDetailBody from "../../components/AdminDetail/AdminDetailBody/AdminDetailBody";
import AdminDetailFooter from "../../components/AdminDetail/AdminDetailFooter/AdminDetailFooter";
import AdminService from "../../../../services/AdminService";
import { AdminDetail } from "../../models/AdminDetail";
import styles from './AdminDetailPage.module.css';

const AdminDetailPage = () => {
    const {id} = useParams();
    const [detail, setDetail] = useState({} as AdminDetail)
    
    useEffect(() => {
        const fetch = async() =>{
            if(id !== undefined){
                const response = await AdminService.GetAdminDetail(id);
                console.warn(response);
                setDetail(response.detail);
            } 
        }
        fetch();
    }, [id]);

    
    return <div className={styles.container}>
        <div className={styles.detailContainer}>
            <AdminDetailHeader detail={detail} />
            <AdminDetailBody detail={detail} />
            <AdminDetailFooter detail={detail} />
        </div>
    </div>
}

export default AdminDetailPage;