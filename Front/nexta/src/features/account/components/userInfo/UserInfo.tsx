import { observer } from 'mobx-react';
import authStore from '../../../../stores/AuthStore/authStore';
import styles from './UserInfo.module.css';
import { SubmitHandler, useForm } from 'react-hook-form';
import { UpdateAccountRequest } from '../../../../http/models/account/UpdateAccount';
import Button from '../../../../shared/components/Button/Button';
import AccountService from '../../../../services/AccountService';
import { useNotifications } from '../../../../shared/components/Notifications/Notifications';
import { AuthService } from '../../../../services/AuthService';
import { ConfirmUpdateEmail } from '../confirmUpdateEmail/ConfirmUpdateEmail';
import { useEffect, useState } from 'react';

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
        const response = await AccountService.update(data);
        
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
                const response = await AuthService.sendVerificationCode(data.email);
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
                <Button content='Обновить профиль' className={styles.submitBtn} type='submit' />
            </div>
        </form>
        <ConfirmUpdateEmail 
            onClose={closeModal}
            email={email}
            isOpen={isModalOpen}
        />
    </div>
});