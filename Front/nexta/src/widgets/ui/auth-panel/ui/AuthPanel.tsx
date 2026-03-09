import { useState } from "react"
import { AuthStep } from "../models/AuthStep";
import { AuthData } from "../../../../entities/auth/authData";
import { RegisterFirstStepForm } from "../../../../features/auth/register/firstStep/ui/RegisterFirstStepForm";
import { RegisterSecondStepForm } from "../../../../features/auth/register/secondStep/ui/RegisterSecondStepForm";
import { LoginFirstStepForm } from "../../../../features/auth/login/firstStep/ui/LoginFirstStepForm";
import { LoginSecondStepForm } from "../../../../features/auth/login/secondStep/ui/LoginSecondStepForm";
import { AccessRecoveryFirstStep } from "../../../../features/account/accessRecovery/firstStep/ui/AccessRecoveryFirstStep";
import { AccessRecoverySecondStep } from "../../../../features/account/accessRecovery/secondStep/ui/AccessRecoverySecondStep";

export const AuthPanel = () => {
    const [authStep, setAuthStep] = useState<AuthStep>("loginFirstStep");
    const [authData, setAuthData] = useState<AuthData>();

    const handleChangeStep = (authStep:AuthStep, authData?:AuthData) => {
        setAuthData(authData);
        setAuthStep(authStep);
    }

    return <>
        {authStep === 'loginFirstStep' && (
            <LoginFirstStepForm 
                changeAuth={handleChangeStep}
            />
        )}
        {authStep === 'loginSecondStep' && (
            <LoginSecondStepForm 
                changeAuth={handleChangeStep}
                authData={authData}
            />
        )}
        {authStep === "registerFirstStep" && (
            <RegisterFirstStepForm
                changeAuth={handleChangeStep}
            />
        )}
        {authStep === 'registerSecondStep' && (
            <RegisterSecondStepForm
                authData={authData}
            />
        )}
        {authStep === 'accessRecoveryFirstStep' && (
            <AccessRecoveryFirstStep 
                handleChangeAuth={handleChangeStep}
            />
        )}
        {authStep === 'accessRecoverySecondStep' && (
            <AccessRecoverySecondStep
                handleChangeAuth={handleChangeStep}
                authData={authData}
            />
        )}
    </>
}