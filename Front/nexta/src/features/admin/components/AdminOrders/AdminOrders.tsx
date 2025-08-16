import GetAllOrdersResponse from "../../../../models/admin/GetAllOrders/GetAllOrdersResponse";
import AdminOrderItem from "../AdminOrderItem/AdminOrderItem";

const AdminOrders:React.FC<{response:GetAllOrdersResponse}> = ({response}) => {
    return <div>
        {response.data?.items?.map((order) => 
            <ul key={order.id}>
                <AdminOrderItem 
                    order={order} 
                    key={order.id}
                />
            </ul>
        )}
    </div>
}

export default AdminOrders;