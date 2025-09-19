import { useEffect, useState } from "react"
import Login from "../components/login/Login"
import { RegisterFirstStep } from "../components/registerFirstStep/RegisterFirstStep"
import { RegisterSecondStep } from "../components/registerSecondStep/RegisterSecondStep"
import CodeVerify from "../components/codeVerify/CodeVerify"
import authStore from "../../../stores/AuthStore/authStore"
import { useNavigate } from "react-router-dom"
import { AccessRecoveryFirstStep } from "../components/accessRecoveryFirstStep/AccessRecoveryFirstStep"
import { AccessRecoverySecondStep } from "../components/accessRecoverySecondStep/AccessRecoverySecondStep"

export type AuthStep = "login" | "registerFirstStep" | "registerSecondStep" | "codeVerify" | "accessRecoveryFirstStep" | "accessRecoverySecondStep"

export interface UserData {
    id?:string | null
    email:string | null
    firstName?:string | null
    lastName?:string | null
    middleName?:string | null
    role?:string | null
    phone?:string | null
    password?: string,
    confirmPassword?: string | null
}

export const AuthPage = () => {
    const navigate = useNavigate();

    useEffect(() => {
        if(authStore.isAuthenticated){
            navigate('/');
        }
    }, []);

    const [authStep, setAuthStep] = useState<AuthStep>("login");
    const [authUser, setAuthUser] = useState<UserData>({} as UserData);

    const handleChangeAuth = (authStep:AuthStep, authUser: UserData) => {
        console.warn(authUser);
        setAuthStep(authStep);
        setAuthUser(authUser);
    }

    return <div>
        {authStep === "login" && (
            <Login 
                handleChangeAuth={handleChangeAuth}
            />
        )}
        {authStep === "registerFirstStep" &&
            <RegisterFirstStep 
                handleChangeAuth={handleChangeAuth}
            />
        }
        {authStep === "registerSecondStep" &&
            <RegisterSecondStep 
                authUser={authUser}
            />
        }
        {authStep === 'codeVerify' && 
            <CodeVerify 
                authUser={authUser}
            />
        }
        {authStep === 'accessRecoveryFirstStep' &&
            <AccessRecoveryFirstStep
                handleChangeAuth={handleChangeAuth}
            />
        }
        {authStep === 'accessRecoverySecondStep' &&
            <AccessRecoverySecondStep
                authUser={authUser}
                handleChangeAuth={handleChangeAuth}
            />
        }
    </div>
}