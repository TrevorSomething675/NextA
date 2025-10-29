import { useEffect, useState } from "react"
import { NotificationItem } from "./NotificationItem/NotificationItem";
import styles from './NotificationsContainer.module.css';
import authStore from "../../../shared/stores/auth/authStore";
import { NotificationApi } from "../../../shared/http/notification/notificationApi";
import { Notification } from "../../../entities/user/models/notification";

export const NotificationsContainer = () => {
    const [notifications, setNotifications] = useState<Notification[]>();

    useEffect(() => {
        fetchData();
    }, [])
    
    const fetchData = async() => {
        const userId = authStore.user.id ?? '';
        const response = await NotificationApi.Get(userId);

        if(response.success && response.status === 200){
            setNotifications(response.data.items);
        }
    }

    return <div className={styles.container}>
        {notifications && notifications?.length > 0 && notifications.map(notification => 
            <NotificationItem key={notification.id} notification={notification} />
        )}
    </div>
}