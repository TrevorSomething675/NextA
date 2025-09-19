import { SubmitHandler, useForm } from 'react-hook-form';
import styles from './ChangePassword.module.css';
import { observer } from 'mobx-react';
import { useState } from 'react';
import { ChangePasswordRequest } from '../../../../http/models/auth/ChangePassword';
import Button from '../../../../shared/components/Button/Button';
import authStore from '../../../../stores/AuthStore/authStore';
import { useNavigate } from 'react-router-dom';
import { useNotifications } from '../../../../shared/components/Notifications/Notifications';
import { AuthService } from '../../../../services/AuthService';


export const ChangePassword = observer(() => {
    const {register, handleSubmit, formState: {errors}} = useForm<ChangePasswordRequest>();
    const [hasError, setError] = useState('');
    const [isLoading, setLoading] = useState(false);
    const navigate = useNavigate();
    const { addNotification } = useNotifications();

    const submit: SubmitHandler<ChangePasswordRequest> = async (data: ChangePasswordRequest) => {
        setLoading(true);

        data.email = authStore?.user?.email ?? '';
        data.userId = authStore?.user?.id ?? '';

        try{
            const response = await AuthService.ChangePassword(data);
            if(response.success && response.status === 200){
                authStore.logout();
                authStore.setAdminStatus(false);
                AuthService.logout();
                setError('');
                
                addNotification({
                    header: 'Пароль успешно изменён',
                    body: 'Пожалуйста, выполните вход в систему повторно, чтобы мы смогли обновить информацию.'
                })
                navigate('/Auth')
            } else if(!response.success) {
                setError(response.data.Message ?? '');
            }
            setLoading(false);
        }
        finally{
            setLoading(false);
        }
    }

    return <div className={styles.container}>
        <form className={styles.form} onSubmit={handleSubmit(submit)}>
            <h2 className={styles.h2}>Смена пароля</h2>
            <div>
                <label
                    className={styles.label}
                    htmlFor='password'>Старый пароль:</label>
                <input id='legacyPassword' type='password' className={styles.input} {...register('legacyPassword', {
                    required: 'Введите старый пароль',
                })} />
                {errors?.password && <div className={styles.error}>{errors.legacyPassword?.message}</div>}
                <label
                    className={styles.label}
                    htmlFor='password'>Пароль:</label>
                <input id='password' type='password' className={styles.input} {...register('password', {
                    required: 'Введите пароль',
                })} />
                {errors?.password && <div className={styles.error}>{errors.password?.message}</div>}
                <label
                    className={styles.label}
                    htmlFor='password'>Подтвердите пароль:</label>
                <input id='confirmPassword' type='password' className={styles.input} {...register('confirmPassword', {
                    required: 'Подтвердите новый пароль',
                })} />
                {errors?.password && <div className={styles.error}>{errors.confirmPassword?.message}</div>}
                {hasError && 
                <div className={styles.error}> 
                    {hasError} 
                </div>}
                <div className={styles.btnsContainer}>
                    {isLoading ? 
                        (<img src="/loading2.gif" className={styles.loading} />)
                        : 
                        (<p className={styles.p}>
                            <Button content='Сменить пароль' className={styles.confirmPasswordBtn} type={'submit'} />
                        </p>)
                    }
                </div>
            </div>
        </form>
    </div>
});