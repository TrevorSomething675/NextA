import { observer } from 'mobx-react';
import { useEffect, useState } from 'react';
import OrderService from '../../../../services/OrderService';
import { GetOrdersForUserFilter, GetOrdersForUserRequest, GetOrdersForUserResponse } from '../../models/GetOrdersForUserFilter';
import Pagging from '../../../../shared/components/Pagging/Pagging';
import OrderItem from '../orderItem/OrderItem';
import authStore from '../../../../stores/AuthStore/authStore';


const Orders = observer(() => {
    const [response, setResponse] = useState<GetOrdersForUserResponse>({} as GetOrdersForUserResponse);
    const handlePageNumberChange = (pageNumber:number) => {
        const userId = authStore?.user?.id ?? '';
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
        const userId = authStore?.user?.id ?? '';
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
        }
    }
    
    return <div>
        {(response?.data?.items !== undefined) ?(

            <ul>
                {response?.data?.items.map((order) => <OrderItem key={order.id} order={order} />)}
            </ul>
            ) : (
                <h2>Заказов нет</h2>
            )}
        {response?.data !== undefined && <Pagging pageCount={response.data.pageCount} onPageNumberChange={handlePageNumberChange}/>}
    </div>
});

export default Orders;