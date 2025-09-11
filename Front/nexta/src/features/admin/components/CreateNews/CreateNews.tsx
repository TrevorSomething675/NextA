import { useState } from "react";
import Image from "../../../../shared/components/Image/Image";
import styles from './CreateNews.module.css';
import { SubmitHandler, useForm } from "react-hook-form";
import AdminService from "../../../../services/AdminService";
import { useNotifications } from "../../../../shared/components/Notifications/Notifications";
import { AddNewsRequest } from "../../../../http/models/news/AddNews";

interface CreateNewsProps {
    fetchData: () => Promise<void>;
}

const CreateNews: React.FC<CreateNewsProps> = ({ fetchData }) => {
    const { register, setValue, handleSubmit, formState: {errors}, reset } = useForm<AddNewsRequest>();
    const [previewImage, setPreviewImage] = useState<string | null>(null);
    const { addNotification } = useNotifications();
    const [error, setError] = useState<string>('');

    const handleOnChangeImage = (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0]
        if(!file) return;

        const name = file.name;
        const reader = new FileReader();
        reader.onload = (event) => {
            const base64String = event.target?.result?.toString().split(',')[1];
            setPreviewImage(base64String!);
            setValue('imageBase64String', base64String!);
            setValue('imageName', name);
        }
        reader.readAsDataURL(file);
    }

    const submit: SubmitHandler<AddNewsRequest> = async (data: AddNewsRequest) => {
        if(data.imageName === undefined) {
            setError('Необходимо добавить картинку для новости');
            return;
        }
        if(data.imageName === null){
            setError('Картинка не должна быть пустой');
            return;
        }
        setError('');
        
        await AdminService.AddNews(data);
        addNotification({
            header: 'Новость успешно добавлена'
        });
        
        setPreviewImage(null);
        reset();
        await fetchData();
    }

    return <div className={styles.container}>
        <form className={styles.form} onSubmit={handleSubmit(submit)}>
            <div className={styles.imageContainer}>
                <Image isLoading={false} base64String={previewImage ?? undefined} isBase64Image={true} className={styles.image} />
                <input className={styles.imageInput}
                    type="file"
                    accept="image/*"
                    onChange={handleOnChangeImage}
                />
            </div>
            <div className={styles.newsItems}>
                <div>
                    <label>Заголовок: </label>
                    <input className={styles.input} {...register('header')} />
                </div>
                <div className={styles.textAreaItem}>
                    <label>Тело: </label>
                    <textarea className={styles.textArea} {...register('description')} />
                </div>
                <div>
                    {error &&
                        <p className={styles.error}>
                            {error}
                        </p>
                    }
                </div>
                <div className={styles.sumbitContainer}>
                    <button type="submit" className={styles.submitBtn}>Создать</button>
                </div>
            </div>
        </form>
    </div>
}

export default CreateNews;