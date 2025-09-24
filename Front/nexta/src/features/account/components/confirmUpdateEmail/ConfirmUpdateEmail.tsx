import { SubmitHandler, useForm } from 'react-hook-form';
import { ConfirmUpdateEmailRequest } from '../../../../http/models/account/ConfirmUpdateEmail';
import styles from './ConfirmUpdateEmail.module.css';
import AccountService from '../../../../services/AccountService';
import Button from '../../../../shared/components/Button/Button';
import authStore from '../../../../stores/AuthStore/authStore';
import { useNotifications } from '../../../../shared/components/Notifications/Notifications';
import { useEffect, useState } from 'react';
import { AuthService } from '../../../../services/AuthService';

interface ConfirmUpdateEmailProps {
    onClose: () => void;
    email:string;
    isOpen: boolean;
}

export const ConfirmUpdateEmail = ({ onClose, email, isOpen }: ConfirmUpdateEmailProps) => {
    const { register, handleSubmit, formState: { errors } } = useForm<ConfirmUpdateEmailRequest>();
    const { addNotification } = useNotifications();
    const [hasError, setError] = useState('');
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

    const submit: SubmitHandler<ConfirmUpdateEmailRequest> = async (data: ConfirmUpdateEmailRequest) => {
        const newEmail = email;
        const legacyEmail = authStore?.user?.email!;
        data.email = newEmail;
        data.legacyEmail = legacyEmail;
        const response = await AccountService.updateEmail(data);
        if (response.success && response.status === 200) {
            setError('');
            authStore.setUserData(response.data.user);
            addNotification({
                header: 'Запрос на изменение почты',
                body: 'Необходимо ввести код подтверждения для изменения почты'
            });
        } else if(!response.success && response.status === 400) {
            setError(response.data.Message ?? '');
        }
    };

    const handleSendCodeAgain = () => {
        if(isDisabled)
            return;

        setIsDisabled(true);
        setCountdown(30);
        AuthService.sendVerificationCode(email);
    }

    return (
        <div className={`${styles.confirmForm} ${isOpen ? styles.open : styles.closed}`}>
            <button
                type="button"
                className={styles.closeButton}
                onClick={onClose}
                aria-label="Закрыть"
            >
                ×
            </button>

            <form onSubmit={handleSubmit(submit)}>
                <h2 className={styles.h2}>Подтверждение</h2>
                
                <label className={styles.confirmLabel} htmlFor="emailCode">
                    Код:
                </label>
                <input
                    id="emailCode"
                    type="text"
                    className={styles.confirmInput}
                    {...register('code', {
                        required: 'Введите код подтверждения',
                    })}
                />
                {errors.code && <p className={styles.error}>{errors.code.message}</p>}
                {hasError && <div className={styles.error}>{hasError}</div>}

                <div className={styles.description}>
                    На вашу новую почту было отправлено письмо с кодом подтверждения.
                </div>

                <div className={styles.footer}>
                    <Button content="Подтвердить" className={styles.confirmBtn} type="submit" />
                    <button 
                            className={`${styles.sendCodeAgainBtn} ${isDisabled ? styles.disabled : ''}`} 
                            onClick={handleSendCodeAgain} 
                            type='button'
                            disabled={isDisabled}
                        >
                            {isDisabled ? `Повторная отправка: ${countdown}` : 'Отправить код повторно'}
                    </button>
                </div>
            </form>
        </div>
    );
};