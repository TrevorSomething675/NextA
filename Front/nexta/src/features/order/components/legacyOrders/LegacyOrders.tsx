import { useEffect, useState } from "react";
import { GetOrdersForUserFilter, GetOrdersForUserResponse } from "../../../../http/models/order/GetOrdersForUserFilter";
import { GetLegacyOrdersForUserRequest } from "../../../../http/models/order/GetLegacyOrders";
import OrderService from "../../../../services/OrderService";
import Pagging from "../../../../shared/components/Pagging/Pagging";
import OrderItem from "../orderItem/OrderItem";
import authStore from "../../../../stores/AuthStore/authStore";

const LegacyOrders = () => {
    const [response, setResponse] = useState<GetOrdersForUserResponse>({} as GetOrdersForUserResponse);
    const handlePageNumberChange = (pageNumber:number) => {
        console.warn(response);
        const userId = authStore?.user?.id ?? '';
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
        const userId = authStore?.user?.id ?? '';
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

    return <div>
        {response.data !== undefined && response.data.items.length > 0 &&
            <ul>
                {response.data.items.map((order) => <OrderItem key={order.id} order={order} />)}
            </ul>
        }
        {response?.data !== undefined && <Pagging pageCount={response.data.pageCount} onPageNumberChange={handlePageNumberChange}/>}
    </div>
}

export default LegacyOrders;