import { useState } from 'react';
import { AdminAddDetailToOrderRightBar } from '../../components/AdminAddDetailToOrderRightBar/AdminAddDetailToOrderRightBar';
import AdminOrders from '../../components/AdminOrders/AdminOrders';
import styles from './AdminOrdersPage.module.css';
import { Detail } from '../../../../shared/entities/Detail';

const AdminOrdersPage = () => {
    const [selectedOrderId, setSelectedOrderId] = useState<string | null>(null);
    const [showDetailPanel, setShowDetailPanel] = useState(false);

    const handleOnAddToOrder = (detail:Detail, count:number) => {
        
        detail.count = count;
        console.warn(detail);
        console.error(selectedOrderId);
    }

    return (
        <div className={styles.container}>
            <AdminOrders 
                    onAddDetailClick={(orderId) => {
                    setSelectedOrderId(orderId);
                    setShowDetailPanel(true);
                }}/>
            {showDetailPanel && selectedOrderId && (
                <div className={styles.details}>
                    <AdminAddDetailToOrderRightBar
                        orderId={selectedOrderId}
                        onClose={() => setShowDetailPanel(false)}
                        onAddToOrder={handleOnAddToOrder}
                    />
                </div>
            )}
        </div>
    );
}

export default AdminOrdersPage;