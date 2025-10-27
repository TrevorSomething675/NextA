import { observer } from 'mobx-react';
import { useEffect, useState } from 'react';
import Pagging from '../../../../../sharedLegacy/components/Pagging/Pagging';
import OrderItem from '../OrderItem/OrderItem';
import styles from './Orders.module.css';
import authStore from '../../../../../shared/stores/auth/authStore';
import { OrderApi } from '../../../../../entities/order/api/orderApi';
import { Order } from '../../../../../entities/order/models/order';

const Orders = observer(() => {
    const [orders, setOrders] = useState<Order[]>([]);
    const [pageCount, setPageCount] = useState<number>(1);

    const handlePageNumberChange = (pageNumber:number) => {
        const userId = authStore?.user?.id ?? '';
        fetchData(userId, 8, pageNumber);
    }
    
    useEffect(() => {
        const userId = authStore?.user?.id ?? '';
        fetchData(userId, 8, 1);
    }, []);
    
    const fetchData = async (userId:string, pageSize?:number, pageNumber?:number) => {
        const response = await OrderApi.GetByUserId(userId, [], pageNumber, pageSize);
        if(response.success && response.status === 200){
            setOrders(response.data.items);
            setPageCount(response.data.pageCount);
        }
    }
    
    return <div>
        {(orders !== undefined && orders.length > 0) ? (
            <ul>
                {orders?.map((order) => <OrderItem key={order.id} order={order} />)}
            </ul>
            ) : (
                <div className={styles.noOrders}>
                    У вас пока нет заказов.
                </div>
            )}
        {orders !== undefined && <Pagging pageCount={pageCount} onPageNumberChange={handlePageNumberChange}/>}
    </div>
});

export default Orders;