import { observer } from "mobx-react";
import styles from './AccountPage.module.css';
import Orders from "../../../widgets/ui/order/ui/Orders/Orders";
import { UserInfo } from "../../../featuresLegacy/account/components/userInfo/UserInfo";
import { ConfirmPhone } from "../../../featuresLegacy/account/components/confirmPhone/ConfirmPhone";
import { ChangePassword } from "../../../featuresLegacy/account/components/changePassword/ChangePassword";

export const AccountPage = observer(() => {
    return <div className={styles.container}>
        <h2 className={styles.h2}>Профиль</h2>
            <div className={styles.userContainer}>
                <div className={styles.userInfoContainer}>
                    <UserInfo />
                    <ConfirmPhone />
                </div>
                <ChangePassword />
            </div>
            <div className={styles.legacyOrdersContainer}>
                <h2 className={styles.h2}>История заказов</h2>
                <Orders />
            </div>
        </div>
});