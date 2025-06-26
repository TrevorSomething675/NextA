import { useState } from "react";
import Login from "../components/login/Login";
import Register from "../components/register/Register";
import CodeVerify from "../components/codeVerify/CodeVerify";
import { AuthUser } from "../../../stores/AuthStore/models/AuthUser";

const AuthPage = () => {
    const [isLogin, changeAuth] = useState(true);
    const [isVerifyCode, setVerifyCodeAuth] = useState(false);
    const [authRequest, setAuthRequest] = useState<AuthUser | null>(null);

    const changeLoginStatusHandler = () => {
        changeAuth(!isLogin);
    }

    const changeCodeVerifyHandler = (user: AuthUser) => {
        setAuthRequest(user);
        setVerifyCodeAuth(!isVerifyCode);
    }

    return <div className='page-body'>
            {!isVerifyCode && <>
                {(isLogin) ? 
                (<Login 
                    changeCodeVerifyStatus={changeCodeVerifyHandler} 
                    changeAuthStatus={changeLoginStatusHandler} 
                />)
                :
                (<Register 
                    changeCodeVerifyStatus={changeCodeVerifyHandler} 
                    changeFormStatus={changeLoginStatusHandler} 
                />)}
            </>}
            {isVerifyCode && <CodeVerify firstStepUser={authRequest!} />}
        </div>
}

export default AuthPage;