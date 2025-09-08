import { useEffect, useState } from "react";
import { AdminProduct } from "../../../../models/AdminProduct";
import { ProductStatus } from "../../../../../../models/Product";
import { SubmitHandler, useForm } from "react-hook-form";
import { UpdateAdminProductRequest } from "../../../../models/AdminProduct/UpdateAdminProduct";
import AdminService from "../../../../../../services/AdminService";
import styles from './AdminProductBody.module.css';
import Image from "../../../../../../shared/components/Image/Image";
import Button from "../../../../../../shared/components/Button/Button";

const statusLabels = {
    [ProductStatus.Unknown]: 'Неизвестный статус',
    [ProductStatus.InStock]: 'Есть на складе',
    [ProductStatus.OutOfStock]: 'Нет на складе',
};

export const AdminProductBody: React.FC<{ product: AdminProduct, onUpdate: () => Promise<void> }> = ({ product, onUpdate }) => {
    const [count, setCount] = useState(1);
    const [isEdit, setIsEdit] = useState(false);
    const [isVisible, setVisible] = useState<boolean>(product.isVisible);
    const { register, handleSubmit, setValue, reset } = useForm<UpdateAdminProductRequest>();

    const [productCount, setProductCount] = useState<number>(product.count);
    const [oldPrice, setOldPrice] = useState<number>(product?.oldPrice || 0);
    const [price, setPrice] = useState<number>(product.newPrice);
    const [image, setImage] = useState<string>(product.base64String || '');
    const [imageName, setImageName] = useState<string>('');
    const [imageId, setImageId] = useState<string | undefined>(product.imageId || undefined);

    const [initialValues, setInitialValues] = useState({
        count: product.count,
        oldPrice: product.oldPrice || 0,
        price: product.newPrice,
        imageId: product.imageId,
        imageBase64String: product.base64String || '',
        imageName: product.imageName || '',
        isVisible: product.isVisible,
        name: product.name,
        article: product.article,
        description: product.description,
        status: product.status,
    });

    useEffect(() => {
        if (product) {
            setVisible(product.isVisible);
            setImage(product.base64String || '');
            setImageId(product.imageId || undefined);
            setImageName(product.imageName || '');

            setInitialValues({
                count: product.count,
                oldPrice: product.oldPrice || 0,
                price: product.newPrice,
                imageId: product.imageId,
                imageBase64String: product.base64String || '',
                imageName: product.imageName || '',
                isVisible: product.isVisible,
                name: product.name,
                article: product.article,
                description: product.description,
                status: product.status,
            });

            setValue('imageId', product.imageId ?? '');
        }
    }, [product, setValue]);

    const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0];
        if (!file) return;

        const reader = new FileReader();
        reader.onload = (event) => {
            if (event.target?.result) {
                const base64string = event.target.result.toString().split(',')[1];
                setImage(base64string);
                setImageName(file.name);
                setImageId(undefined);
            }
        };
        reader.readAsDataURL(file);
    };

    const handleRemoveImage = () => {
        setImage('');
        setImageName('');
        setImageId(undefined);

        const fileInput = document.querySelector('input[type="file"]') as HTMLInputElement;
        if (fileInput) {
            fileInput.value = '';
        }
    };

    const getOperationType = (): 0 | 1 | 2 => {
        const hadImage = !!initialValues.imageId;
        const hasImageNow = !!image;

        if (!hadImage && hasImageNow) {
            return 1;
        }
        if (hadImage && !hasImageNow) {
            return 2;
        }
        if (hadImage && hasImageNow) {
            return 0;
        }
        if (!hadImage && !hasImageNow) {
            return 0;
        }
        return 0;
    };

    const submit: SubmitHandler<UpdateAdminProductRequest> = async (data) => {
        const type = getOperationType();

        const payload: UpdateAdminProductRequest = {
            id: product.id,
            name: data.name,
            article: data.article,
            description: data.description,
            status: data.status,
            count: data.count,
            newPrice: data.newPrice,
            oldPrice: data.oldPrice,
            isVisible: isVisible ?? false,
            type,
            imageId: imageId ?? undefined,
            imageName: imageName || undefined,
            imageBase64String: image || undefined,
        };

        const updateProductResponse = await AdminService.UpdateAdminProduct(payload);
        if (updateProductResponse.success && updateProductResponse.status === 200) {
            onUpdate();
            setIsEdit(false);
        }
    };

    const increment = () => setCount(count => count + 1);
    const decrement = () => setCount(count => Math.max(1, count - 1));

    const handleChangeEditStatus = () => {
        setProductCount(product.count);
        setOldPrice(product.oldPrice ?? 0);
        setPrice(product.newPrice);
        setIsEdit(true);
    };

    const handleRejectBtn = () => {
        setProductCount(initialValues.count);
        setOldPrice(initialValues.oldPrice);
        setPrice(initialValues.price);
        setImage(initialValues.imageBase64String);
        setImageName(initialValues.imageName);
        setImageId(initialValues.imageId);
        setVisible(initialValues.isVisible);

        reset({
            name: initialValues.name,
            article: initialValues.article,
            description: initialValues.description,
            status: initialValues.status,
            count: initialValues.count,
            newPrice: initialValues.price,
            oldPrice: initialValues.oldPrice,
            isVisible: initialValues.isVisible,
            imageName: initialValues.imageName,
            imageBase64String: initialValues.imageBase64String,
            imageId: initialValues.imageId,
        });

        const fileInput = document.querySelector('input[type="file"]') as HTMLInputElement;
        if (fileInput) {
            fileInput.value = '';
        }

        setIsEdit(false);
    };

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
            setProductCount(value);
        }
    };

    return (
        <div className={styles.container}>
            <form onSubmit={handleSubmit(submit)}>
                {isEdit ? (
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
                                <button
                                    className={styles.removeImageBtn}
                                    type="button"
                                    onClick={handleRemoveImage}
                                >
                                    Удалить картинку
                                </button>
                            </div>
                        </div>
                        <div className={styles.productContainer}>
                            <ul className={styles.ul}>
                                <li className={styles.headerProduct}>
                                    <span className={styles.headerProductItem}>
                                        Название:{' '}
                                        <input
                                            defaultValue={product.name}
                                            className={styles.input}
                                            {...register('name')}
                                        />
                                    </span>
                                    <span className={styles.headerProductItem}>
                                        Видимый товар?{' '}
                                        <input
                                            type="checkbox"
                                            className={styles.checkBox}
                                            checked={isVisible}
                                            onChange={() => setVisible(!isVisible)}
                                        />
                                    </span>
                                </li>
                                <li>
                                    Артикул:{' '}
                                    <input
                                        defaultValue={product.article}
                                        className={styles.input}
                                        {...register('article')}
                                    />
                                </li>
                                <li>
                                    <textarea
                                        defaultValue={product.description}
                                        className={styles.textArea}
                                        {...register('description')}
                                    />
                                </li>
                                <li>
                                    <select
                                        className={styles.input}
                                        defaultValue={product.status}
                                        {...register('status')}
                                    >
                                        <option value={0}>Есть на складе</option>
                                        <option value={1}>Нет на складе</option>
                                    </select>
                                </li>
                                <li>
                                    Кол-во:{' '}
                                    <input
                                        value={productCount}
                                        className={styles.input}
                                        {...register('count')}
                                        onChange={handleInputChange}
                                        onKeyDown={(e) => {
                                            if (
                                                !/[0-9]/.test(e.key) &&
                                                !['Backspace', 'Delete', 'Tab', 'ArrowLeft', 'ArrowRight'].includes(e.key)
                                            ) {
                                                e.preventDefault();
                                            }
                                        }}
                                        inputMode="numeric"
                                    />
                                </li>
                            </ul>
                            <div className={styles.productFooter}>
                                <div className={styles.priceContainer}>
                                    <div className={styles.countContainer}>
                                        <span className={styles.newPrice}>
                                            Цена:{' '}
                                            <input
                                                value={price}
                                                className={styles.priceInput}
                                                {...register('newPrice')}
                                                onChange={handlePriceInputChange}
                                                onKeyDown={(e) => {
                                                    if (
                                                        !/[0-9]/.test(e.key) &&
                                                        !['Backspace', 'Delete', 'Tab', 'ArrowLeft', 'ArrowRight'].includes(
                                                            e.key
                                                        )
                                                    ) {
                                                        e.preventDefault();
                                                    }
                                                }}
                                                inputMode="numeric"
                                            />{' '}
                                            руб.
                                        </span>
                                        <span className={styles.oldPriceInput}>
                                            Старая цена:{' '}
                                            <input
                                                value={oldPrice}
                                                className={styles.priceInput}
                                                {...register('oldPrice')}
                                                onChange={handleOldPriceInputChange}
                                                onKeyDown={(e) => {
                                                    if (
                                                        !/[0-9]/.test(e.key) &&
                                                        !['Backspace', 'Delete', 'Tab', 'ArrowLeft', 'ArrowRight'].includes(
                                                            e.key
                                                        )
                                                    ) {
                                                        e.preventDefault();
                                                    }
                                                }}
                                                inputMode="numeric"
                                            />{' '}
                                            руб.
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                ) : (
                    <div className={styles.productPage}>
                        <div className={styles.imageContainer}>
                            <Image
                                isBase64Image={true}
                                base64String={image}
                                className={styles.image}
                            />
                        </div>
                        <div className={styles.productContainer}>
                            <ul className={styles.ul}>
                                <li> - {product.name}</li>
                                <li> - {product.description}</li>
                                <li> - {statusLabels[product.status]}</li>
                                <li> - Осталось на складе: {product.count}</li>
                            </ul>
                            <div className={styles.productFooter}>
                                <div className={styles.priceContainer}>
                                    <div className={styles.countContainer}>
                                        <div>
                                            <button type="button" className={styles.down} onClick={decrement}>
                                                ◄
                                            </button>
                                            <input
                                                value={count}
                                                type="number"
                                                name="quantity"
                                                min="1"
                                                max="10"
                                                step="1"
                                                className={styles.countInput}
                                                onChange={() => {}}
                                            />
                                            <button type="button" className={styles.up} onClick={increment}>
                                                ►
                                            </button>
                                        </div>
                                        <span className={styles.newPrice}>
                                            {product.newPrice * count} руб.
                                        </span>
                                        {product.oldPrice > 0 && (
                                            <span className={styles.oldPrice}>
                                                {product.oldPrice * count} руб.
                                            </span>
                                        )}
                                    </div>
                                    <button className={styles.buyButton}>В корзину</button>
                                </div>
                            </div>
                        </div>
                    </div>
                )}
                <div className={styles.btnContainer}>
                    <Button content="Редактировать" onClick={handleChangeEditStatus} className={styles.editBtn} />
                    {isEdit && (
                        <>
                            <Button content="Сохранить" className={styles.saveBtn} type="submit" />
                            <Button
                                content="Отменить изменения"
                                className={styles.rejectBtn}
                                onClick={handleRejectBtn}
                                type="button"
                            />
                        </>
                    )}
                </div>
            </form>
        </div>
    );
};