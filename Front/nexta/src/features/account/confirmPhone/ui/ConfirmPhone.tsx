import { SubmitHandler, useForm } from 'react-hook-form';
import styles from './ConfirmPhone.module.css';
import { observer } from 'mobx-react';
import { TrueSvg } from '../../../../widgets/ui/svg/TrueSvg/TrueSvg';
import { FalseSvg } from '../../../../widgets/ui/svg/FalseSvg/FalseSvg';
import authStore from '../../../../shared/stores/auth/authStore';
import { Button } from '../../../../shared/ui';
import { ConfirmPhoneFormRequest } from '../models/confirmPhoneFormRequest';

export const ConfirmPhone = observer(() => {
    const { register, handleSubmit } = useForm<ConfirmPhoneFormRequest>();

    const submit: SubmitHandler<ConfirmPhoneFormRequest> = async (data: ConfirmPhoneFormRequest) => {
        data.email = authStore.user.email ?? '';
        /*
        const response = await AuthService.confirmPhone(data);
        
        if(response.success && response.status === 200){
            localStorage.setItem('phone', response.data.phone);
            authStore.user.phone = response.data.phone;
        } else if (!response.success && response?.status === 400){
            console.error(response.data.Message);
        }
        */
    }

    return <form className={styles.container} onSubmit={handleSubmit(submit)}>
        {(authStore.user.phone && authStore?.user?.phone !== '0') ?
        (<div className={styles.success}>
            <TrueSvg />
            <p>
                Ваш номер телефона подтверждён
            </p>
        </div>)
        :
        (<div>
            <h2 className={styles.h2}>
                <FalseSvg />Номер телефона не указан
            </h2>
            <ul>
                <div>Укажите ваш номер телефона, это ускорит обработку заказов.</div>
                <label className={styles.label}>Номер телефона: </label>
                <input type='text' 
                    {...register('phone')}
                    className={styles.input} 
                />
            </ul>
            <Button className={styles.confirmBtn} type={'submit'}>
                Подтвердить
            </Button>
        </div>)
        }
    </form>
});