import Register from "../../components/auth/register";
import Login from "../../components/auth/login";

import { useState } from "react";

const AuthPage = () => {
    const [isLogin, changeAuth] = useState(true);
    const loginStatusHandler = () => {
        changeAuth(!isLogin);
    }

    return <div className='page-body'>
            {isLogin ? <Login changeFormStatus={loginStatusHandler} /> : <Register changeFormStatus={loginStatusHandler} />}
        </div>
}

export default AuthPage;