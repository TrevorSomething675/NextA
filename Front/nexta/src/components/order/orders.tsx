import { useEffect, useState } from 'react';
import OrderService from '../../services/OrderService';
import auth from '../../stores/auth';
import GetOrdersForUserRequest from '../../models/order/GetOrdersForUserRequest';
import GetOrdersForUserFilter from '../../models/order/GetOrdersForUserFilter';
import OrderItem from './orderItem/orderItem';
import { observer } from 'mobx-react';
import orderStore from '../../stores/orderStore';
import { toJS } from 'mobx';
import Pagging from '../pagging/pagging';
import GetOrdersForUserResponse from '../../models/order/GetOrdersForUserResponse';

const Orders = observer(() => {
    const [response, setResponse] = useState<GetOrdersForUserResponse>({} as GetOrdersForUserResponse);
    const handlePageNumberChange = (pageNumber:number) => {
        const userId = auth?.user?.id;
        const filter:GetOrdersForUserFilter = {
            userId: userId,
            pageSize: 8,
            pageNumber: pageNumber
        };
        const request:GetOrdersForUserRequest ={
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
        const request:GetOrdersForUserRequest = {
            filter:filter
        };
        fetchData(request);
    }, []);
    
    const fetchData = async (request:GetOrdersForUserRequest) => {
        const result = await OrderService.GetOrdersForUser(request);
        if(result?.statusCode == 200){
            setResponse(result.value);
            orderStore.setOrders(result?.value?.orders?.items);
            orderStore.setTotalOrders(result?.value?.totalCount);
        }
    }
    
    return <div>
        {(toJS(orderStore?.orders) !== undefined) && (toJS(orderStore?.orders).length > 0) &&
            <ul>
                {orderStore?.orders?.map((order) => <OrderItem key={order.id} order={order} />)}
            </ul>
        }
        {response?.orders !== undefined && <Pagging pageCount={response.orders.pageCount} onPageNumberChange={handlePageNumberChange}/>}
    </div>
});

export default Orders;