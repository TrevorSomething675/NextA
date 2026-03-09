import { observer } from 'mobx-react';
import styles from './UserInfo.module.css';
import { SubmitHandler, useForm } from 'react-hook-form';
import { useNotifications } from '../../../../shared/contexts/notifications/NotificationsContext';
import { useEffect, useState } from 'react';
import { UpdateAccountRequest } from '../models/updateAccountRequest';
import authStore from '../../../../shared/stores/auth/authStore';
import { AccountApi } from '../../../../shared/http/account/accountApi';
import { VerificationApi } from '../../../../shared/http/account/verification/verificationApi';
import { Button } from '../../../../shared/ui';
import { ConfirmUpdateEmail } from '../../confirmUpdateEmail/ui/ConfirmUpdateEmail';

export const UserInfo = observer(() => {
    const { register, reset, handleSubmit, formState: {errors} } = useForm<UpdateAccountRequest>();
    const { addNotification } = useNotifications();
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [email, setNewEmail] = useState<string>('');

    useEffect(() => {
        if (authStore.user) {
            reset({
                firstName: authStore.user.firstName || '',
                lastName: authStore.user.lastName || '',
                middleName: authStore.user.middleName || '',
                email: authStore.user.email || '',
            });
        }
        setNewEmail(authStore.user.email!);
    }, [authStore.user, reset])
    
    const closeModal = () => setIsModalOpen(false);

    const submit:SubmitHandler<UpdateAccountRequest> = async(data:UpdateAccountRequest) => {
        const userId = authStore.user.id;
        data.id = userId ?? '';
        const response = await AccountApi.Update(data.id, data.firstName, data.lastName, data.middleName, data.email, data.phone)
        
        if(response.success && response.status === 200){
            addNotification({
                header: 'Информация обновлена!'
            });
            
            if(authStore?.user?.email != data.email){
                setNewEmail(data.email);
                addNotification({
                    header: 'Запрос на изменение почты',
                    body: 'Необходимо ввести код подтверждения для изменения почты'
                });
                const response = await VerificationApi.SendVerificationCode(data.email);
                if(response.success && response.status === 200){
                    setIsModalOpen(true);
                }
            }
        }
    }

    return <div className={styles.container}>
        {isModalOpen && <div className={`${styles.overlay} ${styles.open}`} />}
        <h2 className={styles.h2}>Ваши данные</h2>
        <form className={styles.form} onSubmit={handleSubmit(submit)}>
            <div className={styles.userContainer}>
                <ul className={styles.ul}>
                    <li className={styles.li}>
                        Имя: 
                        <input className={styles.input} {...register('firstName')} />
                    </li>
                    <li className={styles.li}>Фамилия:
                        <input className={styles.input} {...register('lastName')} />
                    </li>
                    <li className={styles.li}>Отчество:
                        <input className={styles.input} {...register('middleName')} />
                    </li>
                </ul>
                <ul className={styles.ul}>
                    <li className={styles.li}>Номер телефона: {(authStore.user.phone) ? 
                        (authStore.user.phone)
                         : 
                        (<span className={styles.error}>Отсутствует</span>)
                        }
                    </li>
                    <li className={styles.li}>E-mail:
                        <input className={styles.input} {...register('email')} defaultValue={authStore?.user?.email!} />
                    </li>
                </ul>
            </div>
            <div className={styles.footer}>
                <Button className={styles.submitBtn} type='submit'>
                    Обновить профиль
                </Button>
            </div>
        </form>
        <ConfirmUpdateEmail 
            onClose={closeModal}
            email={email}
            isOpen={isModalOpen}
        />
    </div>
});