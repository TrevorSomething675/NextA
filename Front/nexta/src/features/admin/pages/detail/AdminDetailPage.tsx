import { useCallback, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import AdminService from "../../../../services/AdminService";
import { AdminDetail } from "../../models/AdminDetail";
import styles from './AdminDetailPage.module.css';
import AdminDetailHeader from "../../components/AdminDetail/AdminDetailHeader/AdminDetailHeader";
import AdminDetailBody from "../../components/AdminDetail/AdminDetailBody/AdminDetailBody";

const AdminDetailPage = () => {
    const {id} = useParams();
    const [detail, setDetail] = useState({} as AdminDetail)
    
    const fetchData = useCallback(async () => {
        if (id) {
            const response = await AdminService.GetAdminDetail(id, true);
            setDetail(response.detail);
        }
    }, [id]);

    useEffect(() => {
        fetchData();
    }, [fetchData]);
    
    return <div className={styles.container}>
        <div className={styles.detailContainer}>
            <AdminDetailHeader detail={detail} />
            <AdminDetailBody detail={detail} onUpdate={fetchData}/>
        </div>
    </div>
}

export default AdminDetailPage;