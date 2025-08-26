import { useState } from "react";
import Login from "../components/legacy/login/LLogin";
import Register from "../components/legacy/register/LRegister";
import CodeVerify from "../components/codeVerify/LCodeVerify";
import { AuthUser } from "../../../stores/AuthStore/models/AuthUser";

const LegacyAuthPage = () => {
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

    return <div>
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

export default LegacyAuthPage;