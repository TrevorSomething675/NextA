import { SubmitHandler, useForm } from 'react-hook-form';
import styles from './ChangePassword.module.css';
import { observer } from 'mobx-react';
import { useState } from 'react';
import { ChangePasswordRequest } from '../../../auth/models/changePassword';
import { ErrorResponseModel } from '../../../../shared/models/ErrorResponseModel';
import Button from '../../../../shared/components/Button/Button';
import authStore from '../../../../stores/AuthStore/authStore';
import { AuthService } from '../../../../services/LegacyAuthService';
import { useNavigate } from 'react-router-dom';
import { useNotifications } from '../../../../shared/components/Notifications/Notifications';


export const ChangePassword = observer(() => {
    const {register, handleSubmit, formState: {errors}} = useForm<ChangePasswordRequest>();
    const [hasError, setError] = useState('');
    const [isLoading, setLoading] = useState(false);
    const navigate = useNavigate();
    const { addNotification } = useNotifications();

    const submit: SubmitHandler<ChangePasswordRequest> = async (data: ChangePasswordRequest) => {
        data.email = authStore?.user?.email ?? '';
        data.userId = authStore?.user?.id ?? '';
        const response = AuthService.changePassword(data);
        response.then(() => {
            authStore.logout();
            authStore.setAdminStatus(false);
            AuthService.logout();
            
            addNotification({
                header: 'Пароль успешно изменён',
                body: 'Пожалуйста, выполните вход в систему повторно, чтобы мы смогли обновить информацию.'
            })
            navigate('/Auth')
        })
        .catch(error => {
            const errorResponse = error as ErrorResponseModel;
            setError(errorResponse.message ?? '');
        })
        .finally(() =>{
            setLoading(false);
        })
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