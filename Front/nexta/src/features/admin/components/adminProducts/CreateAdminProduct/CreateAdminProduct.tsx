import { SubmitHandler, useForm } from "react-hook-form";
import { useState } from "react";
import { useNotifications } from "../../../../../shared/components/Notifications/Notifications";
import AdminService from "../../../../../services/AdminService";
import styles from './CreateAdminProduct.module.css';
import Button from "../../../../../shared/components/Button/Button";
import Image from "../../../../../shared/components/Image/Image";
import { CreateAdminProductRequest } from "../../../../../http/models/adminProduct/CreateAdminProduct";

export const CreateAdminProduct = () => {
    const { handleSubmit, setValue, register } = useForm<CreateAdminProductRequest>();
    const [isVisible, setVisible] = useState<boolean>();
    const { addNotification } = useNotifications();
    const [image, setImage] = useState<string>('');

    const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0];
        if(!file) return;

        const reader = new FileReader();
        reader.onload = (event) => {
            if(event.target?.result) {
                const base64string = event.target.result.toString().split(',')[1];
                setValue('imageName', file.name);
                setValue('ImageBase64String', base64string);
                setImage(base64string);
            }
        };
        reader.readAsDataURL(file);
    }
    
    const submit: SubmitHandler<CreateAdminProductRequest> = async (data:CreateAdminProductRequest) => {
        try{
            if (!image) {
                data.imageName = undefined;
            }
            if(!data.oldPrice) {
                data.oldPrice = undefined;
            }
            const createProductResponse = await AdminService.CreateAdminProduct(data);
            if(createProductResponse.success && createProductResponse.status === 200){
                addNotification({
                    header: `Товар ${createProductResponse.data.id} успешно создан`
                })
            }
        } catch(error){
            console.warn(error);
        }
    }

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const value = parseInt(e.target.value, 10);
        
        if (!isNaN(value) && value >= 0) {
            setValue('count', value);
        }
    }

    const handleRemoveImage = () => {
        setImage('');
        setValue('imageName', '');
        setValue('ImageBase64String', '');

        const fileInput = document.querySelector('input[type="file"]') as HTMLInputElement;
        if (fileInput) {
            fileInput.value = '';
        }
    }

    return <div className={styles.container}>
        <form onSubmit={handleSubmit(submit)} className={styles.form}>
            <div className={styles.productPage}>
                <div className={styles.imageContainer}>
                    <Image
                        base64String={image}
                        isBase64Image={true}
                        className={styles.image}
                    />
                    <div className={styles.changeImageContainer}>
                        <input 
                            type="file" 
                            onChange={handleFileChange}
                            accept="image/*"
                        />
                        <button className={styles.removeImageBtn} type='button' onClick={handleRemoveImage}>
                            Удалить картинку
                        </button>
                    </div>
                </div>
                <div className={styles.productContainer}>
                <ul className={styles.ul}>
                    <li className={styles.headerProduct}>
                        <span className={styles.headerProductItem}>
                            Название: <input className={styles.input} {...register('name')} />
                        </span>
                        <span className={styles.headerProductItem}>
                            Видимый товар? 
                            <input
                                type="checkbox"
                                className={styles.checkBox}
                                checked={isVisible}
                                onChange={() => setVisible(!isVisible)}
                            />
                        </span>
                    </li>
                    <li className={styles.il}>
                        Категория: <input className={styles.input} {...register('category')} />
                    </li>
                    <li className={styles.il}>
                        Артикул: <input className={styles.input} {...register('article')} />
                    </li>
                    <li className={styles.il}>
                        <textarea className={styles.textArea} {...register('description')} />
                    </li>
                    <li className={styles.il}>
                        <select className={styles.input} {...register('status')}>
                            <option value={0}>Есть на складе</option>
                            <option value={1}>Нет на складе</option>
                        </select>
                    </li>
                    <li className={styles.il}>
                        Кол-во: <input 
                            className={styles.input}
                            {...register('count')}
                            onChange={handleInputChange}
                            onKeyDown={(e) => {
                                if (!/[0-9]/.test(e.key) && 
                                    !['Backspace', 'Delete', 'Tab', 'ArrowLeft', 'ArrowRight'].includes(e.key)) {
                                    e.preventDefault();
                                }
                            }}
                            inputMode="numeric"
                        />
                    </li>
                </ul>
                <div className={styles.product}>
                    <div className={styles.priceContainer}>
                        <div className={styles.countContainer}>
                            <span className={styles.newPrice}>
                                Цена: 
                                <input
                                    className={styles.priceInput}
                                    {...register('newPrice')}
                                    onKeyDown={(e) => {
                                        if (!/[0-9]/.test(e.key) && 
                                            !['Backspace', 'Delete', 'Tab', 'ArrowLeft', 'ArrowRight'].includes(e.key)) {
                                            e.preventDefault();
                                        }
                                    }}
                                    inputMode="numeric"
                                /> руб.
                            </span>
                                <span className={styles.oldPriceInput}>
                                    Старая цена: 
                                    <input
                                        className={styles.priceInput}
                                        {...register('oldPrice')}
                                        onKeyDown={(e) => {
                                        if (!/[0-9]/.test(e.key) && 
                                            !['Backspace', 'Delete', 'Tab', 'ArrowLeft', 'ArrowRight'].includes(e.key)) {
                                            e.preventDefault();
                                        }
                                    }}
                                    inputMode="numeric"
                                /> руб.
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div className={styles.btnContainer}>
                <Button content='Создать товар' className={styles.createBtn} type='submit' />
            </div>
        </form>
    </div>
}