import { useEffect, useState } from "react";
import authStore from "../../../shared/stores/auth/authStore";
import { NotificationApi } from "../../../shared/http/notification/notificationApi";
import styles from './NotifcationsContainer.module.css';
import { NotificationItem } from "./notificationItem/NotificationItem";
import { UserNotification } from "../../../entities/user/userNotification";

export const NotificationsContainer = () => {
    const [notifications, setNotifications] = useState<UserNotification[]>();

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