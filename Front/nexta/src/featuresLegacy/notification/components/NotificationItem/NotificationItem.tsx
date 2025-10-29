import { useState } from "react";
import styles from './NotificationItem.module.css';
import { Notification } from "../../../../entities/user/models/notification";

export const NotificationItem:React.FC<{notification:Notification}> = ({notification}) => {
    const[isRead, setRead] = useState<boolean>(false);

    return <div className={styles.container}>
        <h2 className={styles.h2}>
            {notification.header}
        </h2>
        <div className={styles.text}>
            {notification.message}
        </div>
    </div>
}