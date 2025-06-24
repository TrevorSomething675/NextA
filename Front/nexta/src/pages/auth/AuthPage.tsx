import Register from "../../components/auth/register";
import Login from "../../components/auth/login";
import { useState } from "react";
import CodeVerify from "../../components/auth/codeVerify/codeVerify";

const AuthPage = () => {
    const [isLogin, changeAuth] = useState(true);
    const [isVerifyCode, setVerifyCodeAuth] = useState(false);
    const changeLoginStatusHandler = () => {
        changeAuth(!isLogin);
    }

    const changeCodeVerifyHandler = () => {
        setVerifyCodeAuth(!isVerifyCode);
    }

    return <div className='page-body'>
            {!isVerifyCode && <>
                {(isLogin) ? 
                (<Login changeCodeVerifyStatus={changeCodeVerifyHandler} changeAuthStatus={changeLoginStatusHandler} />)
                :
                (<Register changeCodeVerifyStatus={changeCodeVerifyHandler} changeFormStatus={changeLoginStatusHandler} />)}
            </>}
            {isVerifyCode && <CodeVerify changeCodeVerifyStatus={changeCodeVerifyHandler} />}
        </div>
}

export default AuthPage;