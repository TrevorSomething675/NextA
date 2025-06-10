import { useEffect, useState } from 'react';
import styles from './legacyOrders.module.css';
import GetOrdersForUserResponse from '../../models/order/GetOrdersForUserResponse';
import GetOrdersForUserFilter from '../../models/order/GetOrdersForUserFilter';
import auth from '../../stores/auth';
import OrderService from '../../services/OrderService';
import GetLegacyOrdersForUserRequest from '../../models/order/getLegacyOrders/GetLegacyOrdersForUserRequest';
import OrderItem from './orderItem/orderItem';
import Pagging from '../pagging/pagging';

const LegacyOrders = () => {
    const [response, setResponse] = useState<GetOrdersForUserResponse>({} as GetOrdersForUserResponse);
    const handlePageNumberChange = (pageNumber:number) => {
        const userId = auth?.user?.id;
        const filter:GetOrdersForUserFilter = {
            userId: userId,
            pageSize: 8,
            pageNumber: pageNumber
        };
        const request:GetLegacyOrdersForUserRequest = {
            filter:filter
        };
        fetchData(request);
    }
    
    useEffect(() => {
        const userId = auth?.user?.id;
        const filter:GetOrdersForUserFilter = {
            userId: userId,
            pageSize: 8,
            pageNumber: 1
        };
        const request:GetLegacyOrdersForUserRequest = {
            filter:filter
        };
        fetchData(request);
    }, []);
    
    const fetchData = async (request:GetLegacyOrdersForUserRequest) => {
        const response = await OrderService.GetLegacyOrdersForUser(request);
        if(response){
            setResponse(response);
        }
    }

    return <div className={styles.container}>
        {response.orders !== undefined && response.orders.items.length > 0 &&
            <ul>
                {response.orders.items.map((order) => <OrderItem key={order.id} order={order} />)}
            </ul>
        }
        {response?.orders !== undefined && <Pagging pageCount={response.orders.pageCount} onPageNumberChange={handlePageNumberChange}/>}
    </div>
}

export default LegacyOrders;