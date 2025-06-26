import { SubmitHandler, useForm } from 'react-hook-form';
import styles from './CodeVerify.module.css';
import { useEffect, useRef, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { AuthService } from '../../services/AuthService';
import { VerifyCodeRequest } from '../../models/verifyCode';
import { ErrorResponseModel } from '../../../../shared/models/ErrorResponseModel';
import authStore from '../../../../stores/AuthStore/authStore';
import { AuthUser } from '../../../../stores/AuthStore/models/AuthUser';

const CODE_LENGTH = 6;

type CodeInputs = {
  code: string[];
};

const CodeVerify: React.FC<{ firstStepUser: AuthUser}> = ({ firstStepUser }) => {
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

    const handleSendCodeAgain = () => {
        if(isDisabled)
            return;

        setIsDisabled(true);
        setCountdown(30);
        AuthService.sendVerificationCode(firstStepUser.email!);
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

        const request: VerifyCodeRequest = {
            email: firstStepUser.email!,
            code: code,
            role: 'User'
        };
        setLoading(true);
        try {
            const response = await AuthService.verifyCode(request);
            if(response && authStore.readyToAuth){
                await authStore.secondStepAuthenticate(firstStepUser);
                navigate('/');
            }
        }
        catch (error) {
            const errorResponse = error as ErrorResponseModel;
            setError(errorResponse.message ?? 'Неверный код');
            inputRefs.current.forEach(input => {
                if (input) input.value = '';
            });
            inputRefs.current[0]?.focus();
        }
        finally {
            reset();
            setLoading(false);
        };
    }
    
    return (
        <div className={styles.container}>
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
                    {errors?.code && <div className={styles.error}>Неверный код</div>}
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
    )
}

export default CodeVerify;