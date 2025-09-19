import { observer } from 'mobx-react';
import { useEffect, useState } from 'react';
import OrderService from '../../../../services/OrderService';
import Pagging from '../../../../shared/components/Pagging/Pagging';
import OrderItem from '../orderItem/OrderItem';
import authStore from '../../../../stores/AuthStore/authStore';
import styles from './Orders.module.css';
import { GetOrdersForUserResponse } from '../../../../http/models/order/GetOrdersForUser';

const Orders = observer(() => {
    const [response, setResponse] = useState<GetOrdersForUserResponse>({} as GetOrdersForUserResponse);

    const handlePageNumberChange = (pageNumber:number) => {
        const userId = authStore?.user?.id ?? '';
        fetchData(userId, 8, pageNumber);
    }
    
    useEffect(() => {
        const userId = authStore?.user?.id ?? '';
        fetchData(userId, 8, 1);
    }, []);
    
    const fetchData = async (userId:string, pageSize?:number, pageNumber?:number) => {
        const response = await OrderService.GetOrdersForUser(userId, pageSize, pageNumber);
        if(response.success && response.status === 200){
            setResponse(response.data);
        }
    }
    
    return <div>
        {(response?.data?.items !== undefined && response?.data?.items.length > 0) ? (
            <ul>
                {response?.data?.items?.map((order) => <OrderItem key={order.id} order={order} />)}
            </ul>
            ) : (
                <div className={styles.noOrders}>
                    У вас пока нет заказов.
                </div>
            )}
        {response?.data !== undefined && <Pagging pageCount={response.data.pageCount} onPageNumberChange={handlePageNumberChange}/>}
    </div>
});

export default Orders;