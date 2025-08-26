import { useState } from "react"
import Login from "../components/login/Login"
import { RegisterFirstStep } from "../components/registerFirstStep/RegisterFirstStep"
import { RegisterSecondStep } from "../components/registerSecondStep/RegisterSecondStep"
import CodeVerify from "../components/codeVerify/CodeVerify"

export type AuthStep = "login" | "registerFirstStep" | "registerSecondStep" | "codeVerify"

export interface UserData {
    id?:string | null
    email:string | null
    firstName:string | null
    lastName:string | null
    middleName:string | null
    role?:string | null
    phone?:string | null
    password: string,
    confirmPassword?: string | null
}

export const AuthPage = () => {
    const [authStep, setAuthStep] = useState<AuthStep>("login");
    const [authUser, setAuthUser] = useState<UserData>({} as UserData);

    const handleChangeAuth = (authStep:AuthStep, authUser: UserData) => {
        console.log(authStep);
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
    </div>
}