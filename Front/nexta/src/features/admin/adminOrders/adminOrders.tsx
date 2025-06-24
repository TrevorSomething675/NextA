import { useEffect, useState } from 'react';
import GetAllOrdersFilter from '../../../models/admin/GetAllOrders/GetAllOrdersFilter';
import GetAllOrdersRequest from '../../../models/admin/GetAllOrders/GetAllOrdersRequest';
import AdminService from '../../../services/AdminService';
import styles from './AdminOrders.module.css';
import GetAllOrdersResponse from '../../../models/admin/GetAllOrders/GetAllOrdersResponse';
import AdminOrderItem from '../adminOrder/adminOrderItem';

const AdminOrders = () => {
    const [response, setResponse] = useState({} as GetAllOrdersResponse);

    useEffect(() => {
        fetchData();
    }, []);

    const fetchData = async () => {
        const filter:GetAllOrdersFilter = {
            statuses: [0, 1, 2, 3, 4, 5]
        };
        
        const request:GetAllOrdersRequest = {
            filter:filter
        };
        try{
            const response = await AdminService.GetAllOrders(request);
            setResponse(response);
        }
        catch(error){
            console.error(error);
        }
    }

    return <div className={styles.container}>
        {response.orders !== undefined && response?.orders.items.map((order) => 
            <ul>
                <AdminOrderItem order={order} key={order.id} />
            </ul>
        )}
    </div>
}

export default AdminOrders;