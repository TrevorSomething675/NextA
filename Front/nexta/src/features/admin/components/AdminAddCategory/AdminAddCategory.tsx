import { SubmitHandler, useForm } from 'react-hook-form';
import styles from './AdminAddCategory.module.css';
import { AddCategoryRequest } from '../../../../http/models/categories/AddCategory';
import CategoryService from '../../../../services/CategoryService';
import { useNotifications } from '../../../../shared/components/Notifications/Notifications';

export const AdminAddCategory = () => {
    const { register, handleSubmit, formState: {errors} } = useForm<AddCategoryRequest>();
    const {addNotification} = useNotifications();

    const submit: SubmitHandler<AddCategoryRequest> = async (data: AddCategoryRequest) => {
        const response = await CategoryService.Add(data);

        if(response.success && response.status === 200){
            addNotification({
                header: 'Категория товара была успешно добавлена!'
            })
        }
    }

    return <div className={styles.container}>
        <form onSubmit={handleSubmit(submit)}>
            <div className={styles.body}>
                <label
                    className={styles.label}
                    htmlFor='name'>Новая категория: </label>
                <input id='name' type='text' className={styles.input} {...register('name', {
                    required: 'Введите название категории'
                })} />
                {errors?.name && <div className={styles.error}>{errors.name?.message}</div>}
            </div>
            <div className={styles.footer}>
                <button type='submit'>
                    Создать
                </button>
            </div>
        </form>
    </div>
}