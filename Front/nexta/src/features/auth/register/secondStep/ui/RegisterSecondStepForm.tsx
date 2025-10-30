import { useEffect, useRef, useState } from "react";
import { AuthData } from "../../../../../entities/auth/models/authData";
import { SubmitHandler, useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { VerificationApi } from "../../../../../entities/auth/verification/api/verificationApi";
import { AuthApi } from "../../../../../shared/http/auth/authApi";
import { SetAuthData } from "../../../../../shared/lib/authStorage";
import authStore from "../../../../../shared/stores/auth/authStore";
import styles from './RegisterSecondStepForm.module.css';

interface RegisterSecondStepProps {
    authData?:AuthData;
}

const CODE_LENGTH = 6;

type CodeInputs = {
    code: string[];
};

export const RegisterSecondStepForm:React.FC<RegisterSecondStepProps> = ({authData}) => {
    const { register, handleSubmit, setValue, reset, formState: { errors } } = useForm<CodeInputs>({
        defaultValues: {
            code: Array(CODE_LENGTH).fill('')
        }
    });
    const [hasError, setError] = useState('');
    const navigate = useNavigate();
    const [isLoading, setLoading] = useState(false);
    const inputRefs = useRef<(HTMLInputElement | null)[]>([]);

    const [isDisabled, setIsDisabled] = useState(false);
    const [countdown, setCountdown] = useState(30);

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
    
    const handleInput = (index: number, e: React.ChangeEvent<HTMLInputElement>) => {
        let value = e.target.value;
        
        value = value.replace(/\D/g, '');
        
        if (!value && e.target.value) {
            e.target.value = '';
            return;
        }
        
        if (value) {
            setValue(`code.${index}`, value[0]);
            e.target.value = value[0];
            
            if (index < CODE_LENGTH - 1) {
                inputRefs.current[index + 1]?.focus();
            }
        }
    };

    const handleSendCodeAgain = async() => {
        if(isDisabled)
            return;

        setIsDisabled(true);
        setCountdown(30);
        await VerificationApi.SendVerificationCode(authData?.email ?? '');
    }

    const handleKeyDown = (index: number, e: React.KeyboardEvent<HTMLInputElement>) => {
        if (e.key === 'Backspace') {
            if (!e.currentTarget.value && index > 0) {
                inputRefs.current[index - 1]?.focus();
                setValue(`code.${index - 1}`, '');
                if (inputRefs.current[index - 1]) {
                    inputRefs.current[index - 1]!.value = '';
                }
            }
            else if (e.currentTarget.value) {
                setValue(`code.${index}`, '');
                e.currentTarget.value = '';
            }
        }
        
        if (e.key === 'ArrowLeft' && index > 0) {
            inputRefs.current[index - 1]?.focus();
            e.preventDefault();
        }
        
        if (e.key === 'ArrowRight' && index < CODE_LENGTH - 1) {
            inputRefs.current[index + 1]?.focus();
            e.preventDefault();
        }
    };

    const submit: SubmitHandler<CodeInputs> = async (data) => {
        const code = data.code.join('');
        setLoading(true);
        try {
            if(authData){
                const response = await AuthApi.Register(
                    authData.email,
                    authData.firstName,
                    authData?.middleName,
                    authData.lastName,
                    authData.password!,
                    authData.confirmPassword!,
                    code
                );

                if (!response.success) {
                    setError(response.data.Message ?? 'Неверный код');
                    reset({ code: Array(CODE_LENGTH).fill('') });

                    inputRefs.current.forEach(input => {
                        if (input) input.value = '';
                    });

                    inputRefs.current[0]?.focus();
                    return;
                }

                if (response.success && response.status === 200) {
                    const data = response.data;
                    SetAuthData(
                        data.id ?? '',
                        data.firstName,
                        data.lastName,
                        data.middleName ?? '',
                        data.email,
                        data.phone,
                        data.accessToken!,
                        data.role
                    );
                    authStore.authenticate(data.id!, data.email, data.firstName, data.lastName, data.middleName, data.role, data.phone);
                    navigate('/');
                }
            }
        } 
        catch (error) {
            setError('Произошла ошибка при отправке кода');

            reset({ code: Array(CODE_LENGTH).fill('') });

            inputRefs.current.forEach(input => {
                if (input) input.value = '';
            });

            inputRefs.current[0]?.focus();
        } 
        finally {
            setLoading(false);
        }
    };

    return <div className={styles.container}>
            <form className={styles.form} onSubmit={handleSubmit(submit)}>
                <div>
                    <label className={styles.label}>
                        Введите код подтверждения из почты:
                    </label>
                    <div className={styles.codeInputContainer}>
                        {Array.from({ length: CODE_LENGTH }).map((_, index) => (
                            <input
                                key={index}
                                type="tel"
                                inputMode="numeric"
                                pattern="[0-9]*"
                                maxLength={1}
                                className={styles.codeInput}
                                {...register(`code.${index}`, {
                                    required: 'Введите код'
                                })}
                                onChange={(e) => handleInput(index, e)}
                                onKeyDown={(e) => handleKeyDown(index, e)}
                                ref={(el) => { inputRefs.current[index] = el }}
                                autoFocus={index === 0}
                            />
                        ))}
                    </div>
                    {hasError && <div className={styles.error}>{hasError}</div>}
                </div>
                <div>
                    <div className={styles.btnsContainer}>
                        <button className={styles.verifyBtn} type='submit' disabled={isLoading}>
                            {isLoading ? 
                                (<img src="/loading2.gif" className={styles.loading} alt="Loading"/>) : 
                                (<span className={styles.p}>Отправить</span>)
                            }
                        </button>
                        <button className={styles.sendCodeAgainBtn} onClick={handleSendCodeAgain} type='button'>
                            {isDisabled ? `Повторная отправка: ${countdown}` : 'Отправить код повторно'}
                        </button>
                    </div>
                </div>
            </form>
        </div>
}