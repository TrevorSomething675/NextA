import { useEffect, useState } from "react"
import styles from './AdminUsersPage.module.css';
import AdminService from "../../../../services/AdminService";
import { AdminUser } from "../../../../models/AdminUser";
import Pagging from "../../../../shared/components/Pagging/Pagging";
import { AdminUserItem } from "../../components/AdminUserItem/AdminUserItem";

export const AdminUsersPage = () => {
    const [users, setUsers] = useState<AdminUser[]>();
    const [totalPageCount, setTotalPageCount] = useState<number>(1);

    const HandlePageChange = (pageNumber:number = 1) => {
        fetch(pageNumber);
    }

    const fetch = async(pageNumber:number = 1) => {
        const response = await AdminService.GetUsers('', pageNumber);
        if(response.success && response.status === 200){
            setUsers(response.data.data.items);
            setTotalPageCount(response.data.data.pageCount);
        }
    }

    useEffect(() => {
        fetch();
    }, []);

    return <div>
        <h2 className={styles.h2}>Пользователи</h2>
        <ul className={styles.ul}>
            {(users !== undefined && users?.length > 0) && (
                users.map(user => 
                    <AdminUserItem key={user.id} user={user} />
                )
            )}
        </ul>
        <Pagging 
            pageCount={totalPageCount}
            onPageNumberChange={HandlePageChange}
        />
    </div>
}