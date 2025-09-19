import { useState } from "react";
import { UserNotification } from "../../models/UserNotification"
import styles from './NotificationItem.module.css';

export const NotificationItem:React.FC<{notification:UserNotification}> = ({notification}) => {
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