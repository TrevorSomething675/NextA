import { observer } from 'mobx-react';
import { useEffect, useState } from 'react';
import auth from '../../../../stores/auth';
import OrderService from '../../../../services/OrderService';
import orderStore from '../../../../stores/orderStore';
import { toJS } from 'mobx';
import { GetOrdersForUserFilter, GetOrdersForUserRequest, GetOrdersForUserResponse } from '../../models/GetOrdersForUserFilter';
import Pagging from '../../../../shared/components/Pagging/Pagging';
import { OrderItem } from '../orderItem/OrderItem';


export const Orders = observer(() => {
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
        const response = await OrderService.GetOrdersForUser(request);
        if(response){
            setResponse(response);
            orderStore.setOrders(response?.orders?.items);
            orderStore.setTotalOrders(response?.totalCount);
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