import { observer } from "mobx-react";
import Account from "../components/Account/Account";

const AccountPage = observer(() => {
    return <div className='page-body'>
        <Account />
    </div>
});

export default AccountPage;