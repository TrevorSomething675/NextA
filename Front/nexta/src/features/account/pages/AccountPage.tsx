import { observer } from "mobx-react";
import LegacyOrders from "../../order/components/legacyOrders/LegacyOrders";
import styles from './AccountPage.module.css';
import { ChangePassword } from "../components/changePassword/ChangePassword";
import { UserInfo } from "../components/userInfo/UserInfo";
import { ConfirmPhone } from "../components/confirmPhone/ConfirmPhone";

const AccountPage = observer(() => {
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
                <LegacyOrders />
            </div>
        </div>
});

export default AccountPage;