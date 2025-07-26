import { useEffect, useState } from "react";
import AdminService from "../../../../services/AdminService";
import GetAllOrdersFilter from "../../../../models/admin/GetAllOrders/GetAllOrdersFilter";
import GetAllOrdersRequest from "../../../../models/admin/GetAllOrders/GetAllOrdersRequest";
import AdminOrderItem from "../AdminOrderItem/AdminOrderItem";
import GetAllOrdersResponse from "../../../../models/admin/GetAllOrders/GetAllOrdersResponse";

const AdminOrders = ({ onAddDetailClick }: { onAddDetailClick: (orderId: string) => void }) => {
    const [response, setResponse] = useState({} as GetAllOrdersResponse);

    useEffect(() => {
        const filter:GetAllOrdersFilter = {
            pageSize: 8,
            pageNumber: 1,
            statuses: [0,1,2,3,4]
        };
        const request:GetAllOrdersRequest = {
            filter:filter
        }
        fetchData(request)
    }, []);

    const fetchData = async(request:GetAllOrdersRequest) =>{
        const response = await AdminService.GetAllOrders(request);
        setResponse(response);
    }
    return <div>
        {response.data?.items?.map((order) => 
            <ul key={order.id}>
                <AdminOrderItem 
                    order={order} 
                    key={order.id}
                    onAddDetailClick={() => onAddDetailClick(order.id)} />
            </ul>
        )}
    </div>
}

export default AdminOrders;