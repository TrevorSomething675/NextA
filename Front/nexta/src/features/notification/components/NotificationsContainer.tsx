import { useEffect, useState } from "react"
import NotificationService from "../../../services/NotificationService"
import { GetNotificationsResponse } from "../../../http/models/notifications/GetNotifications"
import authStore from "../../../stores/AuthStore/authStore";
import { NotificationItem } from "./NotificationItem/NotificationItem";
import styles from './NotificationsContainer.module.css';

export const NotificationsContainer = () => {
    const [notifications, setNotifications] = useState<GetNotificationsResponse>();

    useEffect(() => {
        fetchData();
    }, [])
    
    const fetchData = async() => {
        const userId = authStore.user.id ?? '';
        const response = await NotificationService.Get(userId);

        if(response.success && response.status === 200){
            setNotifications(response.data);
        }
    }

    return <div className={styles.container}>
        {notifications && notifications?.data?.items?.length > 0 && notifications.data.items.map(notification => 
            <NotificationItem key={notification.id} notification={notification} />
        )}
    </div>
}