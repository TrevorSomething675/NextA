import { HeaderAccount } from "./HeaderAccount/HeaderAccount";
import { HeaderBasket } from "./HeaderBasket/HeaderBasket";
import { HeaderOrder } from "./HeaderOrder/HeaderOrder";
import { HeaderExit } from "./HeaderExit/HeaderExit";
import { HeaderAuth } from "./HeaderAuth/HeaderAuth";
import styles from './HeaderNav.module.css';
import { Link } from "react-router-dom";
import { observer } from "mobx-react";
import authStore from "../../../../stores/AuthStore/authStore";

export const HeaderNav = observer(() => {
    return <div className={styles.container}>
        {authStore.isAuthenticated ? <>
            <div className={styles.accountItem}>
                <Link to='/Order'>
                    <HeaderOrder />
                </Link>
            </div>
            <div className={styles.accountItem}>
                <Link to='/Basket'>
                    <HeaderBasket />
                </Link>
            </div>
            <div className={styles.accountItem}>
                <Link to='/Account'>
                    <HeaderAccount />
                </Link>
            </div>
            <div className={styles.accountItem}>
                <Link to='/'>
                    <HeaderExit />
                </Link>
            </div>
        </> : <>
            <div className={styles.authItem}>
                <Link to='/Auth'>
                    <HeaderAuth />
                </Link>
            </div>
        </>}
    </div>
});