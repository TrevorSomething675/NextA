import { SubmitHandler, useForm } from "react-hook-form";
import { AuthData } from "../../../../../entities/auth/models/authData";
import { AuthStep } from "../../../../../widgets/ui/auth-panel/models/AuthStep";
import { useEffect, useState } from "react";
import { ErrorResponseModel } from "../../../../../sharedLegacy/models/ErrorResponseModel";
import { AccessRecoverySecondStepRequest } from "../models/accessRecoverySecondStepRequest";
import { VerificationApi } from "../../../../../entities/auth/verification/api/verificationApi";
import styles from './AccessRecoverySecondStep.module.css';
import { AuthApi } from "../../../../../shared/http/auth/authApi";

type CodeInputs = {
    handleChangeAuth: (step: AuthStep, user:AuthData) => void;
    authData?:AuthData;
};

export const AccessRecoverySecondStep: React.FC<CodeInputs> = ({handleChangeAuth, authData}) => {
    const { register, handleSubmit, reset, watch, formState: { errors } } = useForm<AccessRecoverySecondStepRequest>();

    const [isDisabled, setIsDisabled] = useState(false);
    const [countdown, setCountdown] = useState(30);
    const [hasError, setError] = useState('');
    const [isLoading, setLoading] = useState(false);

    useEffect(() => {
        let timer:any;
        
        if (isDisabled && countdown > 0) {
        timer = setTimeout(() => {
            setCountdown(countdown - 1);
        }, 1000);
        } else if (countdown === 0) {
        setIsDisabled(false);
        }
            
        return () => clearTimeout(timer);
    }, [isDisabled, countdown]);

    const handleSendCodeAgain = async() => {
        if(isDisabled)
            return;

        setIsDisabled(true);
        setCountdown(30);
        await VerificationApi.SendVerificationCode(authData?.email!);
    }

    const submit: SubmitHandler<AccessRecoverySecondStepRequest> = async (data) => {
        const email = authData?.email!;
        const code = data.code;
        const password = data.password;
        const confirmPassword = data.confirmPassword

        try {
            const response = await AuthApi.AccessRecovery(email, code, password, confirmPassword);
            if(response.success && response.status === 200){
                handleChangeAuth('loginFirstStep', {} as AuthData);
            }
        }
        catch (error) {
            const errorResponse = error as ErrorResponseModel;
            setError(errorResponse.Message ?? 'Неверный код');
        }
        finally {
            reset()
            setLoading(false);
        };
    }
    
    return (
        <div className={styles.container}>
            <form className={styles.form} onSubmit={handleSubmit(submit)}>
                <h2 className={styles.h2}>Восстановление доступа</h2>
                <div>
                    <label className={styles.label} htmlFor='code'>Код: </label>
                    <input 
                        id='code' 
                        type='text' 
                        className={styles.input} {...register('code', { 
                            required: 'код обязателен'
                    })} />
                    {errors.password && <div className={styles.error}>{errors.code?.message}</div>}
                    <label className={styles.label} htmlFor='password'>Пароль:</label>
                    <input 
                        id='password' 
                        type='password' 
                        className={styles.input} {...register('password', {
                            minLength: {
                                value: 6,
                                message: 'Пароль должен быть не менее 6 символов'
                            }
                        })} />
                    {errors.password && <div className={styles.error}>{errors.password?.message}</div>}

                    <label className={styles.label} htmlFor='confirmPassword'>Подтвердите пароль:</label>
                    <input
                        id='confirmPassword'
                        type='password'
                        className={styles.input} {...register('confirmPassword', {
                            required: 'Подтвердите пароль',
                            validate: (value:string) => 
                                value === watch('password') || 'Пароли не совпадают'
                        })} />
                    </div>
                <div>
                    <div className={styles.btnsContainer}>
                        <button className={styles.verifyBtn} type='submit' disabled={isLoading}>
                            {isLoading ? 
                                (<img src="/loading2.gif" className={styles.loading} alt="Loading"/>) : 
                                (<span className={styles.p}>Обновить</span>)
                            }
                        </button>
                        <button 
                            className={`${styles.sendCodeAgainBtn} ${isDisabled ? styles.disabled : ''}`} 
                            onClick={handleSendCodeAgain} 
                            type='button'
                            disabled={isDisabled}
                        >
                            {isDisabled ? `Повторная отправка: ${countdown}` : 'Отправить код повторно'}
                        </button>
                    </div>
                </div>
            </form>
        </div>
    )
}