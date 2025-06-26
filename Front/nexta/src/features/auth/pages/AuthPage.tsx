import { useState } from "react";
import { LoginRequest } from "../models/login";
import { RegistrationRequest } from "../models/registration";
import Login from "../components/login/Login";
import Register from "../components/register/Register";
import CodeVerify from "../components/codeVerify/CodeVerify";

const AuthPage = () => {
    const [isLogin, changeAuth] = useState(true);
    const [isVerifyCode, setVerifyCodeAuth] = useState(false);
    const [authRequest, setAuthRequest] = useState<LoginRequest | RegistrationRequest | null>(null);

    const changeLoginStatusHandler = () => {
        changeAuth(!isLogin);
    }

    const changeCodeVerifyHandler = (request:LoginRequest | RegistrationRequest) => {
        setAuthRequest(request);
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
            {isVerifyCode && <CodeVerify firstStepAuthType={authRequest!} />}
        </div>
}

export default AuthPage;