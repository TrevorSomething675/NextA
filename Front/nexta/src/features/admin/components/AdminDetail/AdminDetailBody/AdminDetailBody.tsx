import { useEffect, useState } from "react";
import { DetailStatus } from "../../../../../shared/entities/Detail";
import styles from './AdminDetailBody.module.css';
import { AdminDetail } from "../../../models/AdminDetail";
import { SubmitHandler, useForm } from "react-hook-form";
import { UpdateAdminDetailRequest } from "../../../models/UpdateAdminDetailRequest";
import AdminService from "../../../../../services/AdminService";
import Image from "../../../../../shared/components/Image/Image";
import Button from "../../../../../shared/components/Button/Button";

const statusLabels = {
    [DetailStatus.Unknown]: 'Неизвестный статус',
    [DetailStatus.InStock]: 'Есть на складе',
    [DetailStatus.OutOfStock]: 'Нет на складе',
};

const AdminDetailBody:React.FC<{detail: AdminDetail, onUpdate:() => Promise<void>}> = ({detail, onUpdate}) => {
    const [count, setCount] = useState(1);
    const [isEdit, setIsEdit] = useState(false);
    const [isVisible, setVisible] = useState<boolean>();
    const { register, handleSubmit, setValue } = useForm<UpdateAdminDetailRequest>();

    const [detailCount, setdetailCount] = useState<number>(detail.count);
    const [oldPrice, setOldPrice] = useState<number>(detail?.oldPrice || 0);
    const [price, setPrice] = useState<number>(detail.newPrice);
    const [image, setImage] = useState<string>('');
    const [initialValues, setInitialValues] = useState({
        count: detail.count,
        oldPrice: detail.oldPrice || 0,
        price: detail.newPrice,
        image: detail.image?.base64String || '',
        isVisible: detail.isVisible,
        name: detail.name,
        article: detail.article,
        description: detail.description,
        status: detail.status
    });

    useEffect(() => {
        if(detail){
            setVisible(detail.isVisible);
            setInitialValues({
            count: detail.count,
            oldPrice: detail.oldPrice || 0,
            price: detail.newPrice,
            image: detail.image?.base64String || '',
            isVisible: detail.isVisible,
            name: detail.name,
            article: detail.article,
            description: detail.description,
            status: detail.status
            });
        }
        setImage(detail.image?.base64String || '');

    }, [detail, setValue]);

    const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0];
        if(!file) return;

        const reader = new FileReader();
        reader.onload = (event) => {
            if(event.target?.result) {
                const base64string = event.target.result.toString().split(',')[1];
                setValue('image.name', file.name);
                setValue('image.base64string', base64string);
                setImage(base64string);
            }
        };
        reader.readAsDataURL(file);
    }
    
    const submit: SubmitHandler<UpdateAdminDetailRequest> = async (data:UpdateAdminDetailRequest) => {
        try{
            data.id = detail.id;
            data.isVisible = isVisible ?? false;

            if (!image) {
                data.image = undefined;
            }
            
            const updateDetailResponse = await AdminService.UpdateAdminDetail(data);
            if(updateDetailResponse.detail){
                onUpdate();
                setIsEdit(false);
            } else {
                console.error('Ошибка при изменении детали');
            }
        } catch(error){
            console.warn(error);
        }
    }

    const increment = () => {
        setCount(count => count + 1);
    };

    const decrement = () => {
        setCount(count => Math.max(1, count - 1));
    };
    
    const handleChangeEditStatus = () => {
        setdetailCount(detail.count);
        setOldPrice(detail.oldPrice ?? null);
        setPrice(detail.newPrice);
        setIsEdit(true);
    }

    const handleRejectBtn = () => {
        setdetailCount(initialValues.count);
        setOldPrice(initialValues.oldPrice);
        setPrice(initialValues.price);
        setImage(initialValues.image ?? '');
        setVisible(initialValues.isVisible);

        setValue('name', initialValues.name);
        setValue('article', initialValues.article);
        setValue('description', initialValues.description);
        setValue('status', initialValues.status);
        setValue('count', initialValues.count);
        setValue('newPrice', initialValues.price);
        setValue('oldPrice', initialValues.oldPrice);
        
        const fileInput = document.querySelector('input[type="file"]') as HTMLInputElement;
        if (fileInput) {
            fileInput.value = '';
        }
        
        setIsEdit(false);
    }

    const handleOldPriceInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const value = parseInt(e.target.value, 10);
        
        if (!isNaN(value) && value >= 0) {
            setOldPrice(value);
        }
    };

    const handlePriceInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const value = parseInt(e.target.value, 10);
        
        if (!isNaN(value) && value >= 0) {
            setPrice(value);
        }
    };

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const value = parseInt(e.target.value, 10);
        
        if (!isNaN(value) && value >= 0) {
            setdetailCount(value);
        }
    }

    const handleRemoveImage = () => {
        setImage('');
        setValue('image.name', '');
        setValue('image.base64string', '');

        const fileInput = document.querySelector('input[type="file"]') as HTMLInputElement;
        if (fileInput) {
            fileInput.value = '';
        }
    }

    return <div className={styles.container}>
        <form onSubmit={handleSubmit(submit)}>
        {isEdit ? (
            <div className={styles.detailPage}>
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
                <div className={styles.detailContainer}>
                <ul className={styles.ul}>
                    <li className={styles.headerDetail}>
                        <span className={styles.headerDetailItem}>
                            Название: <input defaultValue={detail.name} className={styles.input} {...register('name')}/>
                        </span>
                        <span className={styles.headerDetailItem}>
                            Видимый товар? 
                            <input 
                                type="checkbox" 
                                className={styles.checkBox} 
                                checked={isVisible}
                                onChange={() => setVisible(!isVisible)} 
                            />
                        </span>
                    </li>
                    <li>
                        Артикул: <input defaultValue={detail.article} className={styles.input} {...register('article')}/>
                    </li>
                    <li>
                        <textarea defaultValue={detail.description} className={styles.textArea} {...register('description')} />
                    </li>
                    <li>
                        <select className={styles.input} defaultValue={detail.status} {...register('status')}>
                            <option value={0}>Есть на складе</option>
                            <option value={1}>Нет на складе</option>
                        </select>
                    </li>
                    <li> 
                        Кол-во: <input 
                            value={detailCount}
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
                <div className={styles.detailFooter}>
                    <div className={styles.priceContainer}>
                        <div className={styles.countContainer}>
                            <span className={styles.newPrice}>
                                Цена: 
                                <input
                                    value={price}
                                    className={styles.priceInput}
                                    {...register('newPrice')}
                                    onChange={handlePriceInputChange}
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
                                        value={oldPrice}
                                        className={styles.priceInput}
                                        {...register('oldPrice')}
                                        onChange={handleOldPriceInputChange}
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
            )
            :
            (<div className={styles.detailPage}>
                <div className={styles.imageContainer}>
                    <Image
                        isBase64Image={true} 
                        base64String={image} 
                        className={styles.image} 
                    />
                </div>
                <div className={styles.detailContainer}>
                    <ul className={styles.ul}>
                        <li> - {detail.name}</li>
                        <li> - {detail.description}</li>
                        <li> - {statusLabels[detail.status]}</li>
                        <li> - Осталось на складе: {detail.count}</li>
                    </ul>
                <div className={styles.detailFooter}>
                    <div className={styles.priceContainer}>
                        <div className={styles.countContainer}>
                            <div>
                                <button type="button" className={styles.down} onClick={decrement}>◄</button>
                                <input
                                    value={count}
                                    type="number"
                                    name="quantity"
                                    min="1"
                                    max="10"
                                    step="1"
                                    className={styles.countInput}
                                    onChange={handleInputChange}
                                    />
                                <button type="button" className={styles.up} onClick={increment}>►</button>
                                </div>
                                <span className={styles.newPrice}>
                                    {detail.newPrice * count} руб.
                                </span>
                                {(detail.oldPrice !== undefined && detail.oldPrice != 0) &&
                                    <span className={styles.oldPrice}>
                                        {detail.oldPrice * count} руб.
                                    </span>
                                }
                            </div>
                            <button className={styles.buyButton}>
                                В корзину
                            </button>
                        </div>
                    </div>
                </div>
            </div>
            )}
            <div className={styles.btnContainer}>
                <Button content='Редактировать' onClick={handleChangeEditStatus} className={styles.editBtn}/>
                {isEdit && <>
                    <Button content='Сохранить' className={styles.saveBtn} type='submit' />
                    <Button content='Отменить изменения' className={styles.rejectBtn} onClick={handleRejectBtn} type='button' />
                </>}
            </div>
        </form>
    </div>
}

export default AdminDetailBody;