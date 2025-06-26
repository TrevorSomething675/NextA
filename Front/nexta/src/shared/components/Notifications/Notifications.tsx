import { createContext, useContext, useState } from "react"
import styles from './Notifications.module.css';

interface WebNotification {
    header:string,
    body:string,
    id?:string,
    duration?:number
}

const NotificationContext = createContext<{
    addNotification: (notification:WebNotification) => void;}>({addNotification: () =>{},});

    export const NotificationsProvider = ({ children }: { children: React.ReactNode }) => {
        const [notifications, setNotifications] = useState<WebNotification[]>([]);

        const removeNotification = (id: string) => {
            const element = document.querySelector(`[data-id="${id}"]`);
            if (element) {
                element.classList.add(styles.leaving);
            }
        
            setTimeout(() => {
                setNotifications(prev => prev.filter(n => n.id !== id));
            }, 500);
        };

    const addNotification = (notification: WebNotification) => {
        const id = Date.now().toString();
        const duration = notification.duration || 5000;
        const newNotification = { ...notification, id };
    
        setNotifications(prev => [...prev, newNotification]);
    
        if (duration > 0) {
            setTimeout(() => {
            removeNotification(id);
            }, duration);
        }
    };

    return <NotificationContext.Provider value={{ addNotification }}>
        {children}
        <div className={styles.container}>
            {notifications.map((notification) => (
            <div 
                key={notification.id}
                data-id={notification.id}
                className={styles.notificationContainer}>
                <button 
                    className={styles.closeButton}
                    onClick={() => removeNotification(notification.id!)}
                    aria-label="Закрыть уведомление">
                </button>
                <h2 className={styles.h2}>{notification.header}</h2>
                <div className={styles.notificationBody}>
                    {notification.body}
                </div>
            </div>
        ))}
        </div>
    </NotificationContext.Provider>
}

export const useNotifications = () => useContext(NotificationContext);