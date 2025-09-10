import { SubmitHandler, useForm } from "react-hook-form";
import { UserData } from "../../pages/AuthPage";
import { useEffect, useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthService } from "../../../../services/AuthService";
import styles from './RegisterSecondStep.module.css';
import { RegistrationRequest } from "../../../../http/models/auth/Registration";
import authStore from "../../../../stores/AuthStore/authStore";
import BasketService from "../../../../services/BasketService";
import basket from "../../../../stores/basket";
import OrderService from "../../../../services/OrderService";
import orderStore from "../../../../stores/orderStore";

interface RegisterSecondStepProps{
    authUser: UserData,
}

const CODE_LENGTH = 6;

type CodeInputs = {
    code: string[];
};

export const RegisterSecondStep:React.FC<RegisterSecondStepProps> = ({authUser}) => {
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
        AuthService.sendVerificationCode(authUser.email!);
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
            const request: RegistrationRequest = {
                email: authUser.email!,
                firstName: authUser.firstName!,
                middleName: authUser.middleName!,
                lastName: authUser.lastName!,
                password: authUser.password!,
                confirmPassword: authUser.confirmPassword!,
                code: code
            }

            var registerResponse = await AuthService.register(request)
            if(!registerResponse.success){
                setError(registerResponse.data.Message ?? '');
            }
            if(registerResponse.success && registerResponse.status === 200){
                authStore.setUserData(registerResponse.data.user);

                const basketResponse = await BasketService.GetBasketProducts(authUser.id!);
                if(basketResponse.success && basketResponse.status === 200){
                    basket.setBasketItems(basketResponse.data.products);

                    const orderResponse = await OrderService.GetOrdersForUser(authUser.id!);
                    if(orderResponse.success && orderResponse.status === 200){
                        orderStore.setOrderItems(orderResponse?.data.data.items);
                    }
                    navigate('/');
                }
            }
        }
        catch (error) {
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