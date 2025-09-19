import { SubmitHandler, useForm } from "react-hook-form";
import { AuthStep, UserData } from "../../pages/AuthPage";
import { useEffect, useState } from "react";
import { AuthService } from "../../../../services/AuthService";
import { ErrorResponseModel } from "../../../../shared/models/ErrorResponseModel";
import styles from './accessRecoverySecondStep.module.css';
import { AccessRecoverySecondStepRequest } from "../../../../http/models/auth/AccessRecovery";

type CodeInputs = {
    authUser: UserData,
    handleChangeAuth: (step: AuthStep, user:UserData) => void;
};

export const AccessRecoverySecondStep: React.FC<CodeInputs> = ({ authUser, handleChangeAuth}) => {
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

    const handleSendCodeAgain = () => {
        if(isDisabled)
            return;

        setIsDisabled(true);
        setCountdown(30);
        AuthService.sendVerificationCode(authUser.email!);
    }

    const submit: SubmitHandler<AccessRecoverySecondStepRequest> = async (data) => {
        const request: AccessRecoverySecondStepRequest = {
            email: authUser.email!,
            code: data.code,
            password: data.password,
            confirmPassword: data.confirmPassword
        };
        try {
            const response = await AuthService.accessRecovery(request);
            if(response.success && response.status === 200){
                handleChangeAuth('login', {} as UserData);
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