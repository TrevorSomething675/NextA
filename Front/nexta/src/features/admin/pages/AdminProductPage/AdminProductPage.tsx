import { useParams } from 'react-router-dom';
import styles from './AdminProductPage.module.css';
import { useEffect, useState } from 'react';
import { AdminProduct } from '../../models/AdminProduct';
import AdminService from '../../../../services/AdminService';
import { useNotifications } from '../../../../shared/components/Notifications/Notifications';
import { Product, ProductStatus } from '../../../../models/Product';
import BasketService from '../../../../services/BasketService';
import authStore from '../../../../stores/AuthStore/authStore';
import basket from '../../../../stores/basket';
import { AddBasketProductRequest } from '../../../../http/models/basketProduct/AddBasketProduct';
import Image from '../../../../shared/components/Image/Image';
import { ViewAlreadyExistProductInBasket } from '../../../../shared/components/ViewAlreadyExistProductInBasket/ViewAlreadyExistProductInBasket';
import { ProductOperationType, UpdateAdminProductRequest } from '../../../../http/models/adminProduct/UpdateAdminProduct';
import { ProductAttributes } from '../../../product/components/ProductAttributes/ProductAttributes';
import { AdminProductAttributes } from '../../components/AdminProductAttributes/AdminProductAttributes';

export const AdminProductPage = () => {
    const statusLabels = {
        [ProductStatus.Unknown]: 'Неизвестный статус',
        [ProductStatus.InStock]: 'Есть на складе',
        [ProductStatus.OutOfStock]: 'Нет на складе',
    };

    const { id } = useParams();
    const [product, setProduct] = useState<AdminProduct>({} as AdminProduct);
    const [originalProduct, setOriginalProduct] = useState<AdminProduct>({} as AdminProduct);
    const [count, setCount] = useState(1);
    const { addNotification } = useNotifications();
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [isEditing, setIsEditing] = useState(false);
    const [isLoading, setIsLoading] = useState(false);

    useEffect(() => {
        const fetchProduct = async () => {
            if (id !== undefined) {
                const response = await AdminService.GetAdminProduct(id);
                if (response.success && response.status === 200) {
                    const fetchedProduct = response.data.product;
                    setProduct(fetchedProduct);
                    setOriginalProduct(JSON.parse(JSON.stringify(fetchedProduct)));
                }
            }
        };
        console.warn(product);
        fetchProduct();
    }, [id]);

    const increment = () => setCount(prev => prev + 1);
    const decrement = () => setCount(prev => Math.max(1, prev - 1));

    const handleProductCountChange = (newCount: number) => {
        setCount(newCount);
    };

    const handleAddToBasket = async () => {
        const request: AddBasketProductRequest = {
            userId: authStore?.user?.id ?? '',
            productId: product.id,
            countToPay: count,
        };

        const response = await BasketService.AddBasketProduct(request);
        if (response.success && response.status === 200) {
            addNotification({
                header: `Товар ${response.data.basketProduct.name} добавлен в корзину`,
            });
            basket.addBasketProduct(response.data.basketProduct);
        } else if (!response.success && response.status === 409) {
            setIsModalOpen(true);
        }
    };

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const value = parseInt(e.target.value, 10);
        setCount(isNaN(value) || value < 1 ? 1 : value);
    };

    const startEditing = () => {
        setIsEditing(true);
    };

    const cancelEditing = () => {
        setProduct(JSON.parse(JSON.stringify(originalProduct)));
        setIsEditing(false);
    };

    const handleFieldChange = (field: keyof AdminProduct, value: any) => {
        setProduct(prev => ({ ...prev, [field]: value }));
    };

    const handleRemoveImage = () => {
        setProduct(prev => ({
            ...prev,
            image: {
                ...prev.image,
                id: undefined,
                base64String: '',
                name: ''
            }
        }));
    };

    const handleImageUpload = (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0];
        if (!file) return;

        const reader = new FileReader();
        reader.onloadend = () => {
            const base64String = reader.result as string;

            setProduct(prev => ({
                ...prev,
                image: {
                    ...prev.image,
                    name: file.name,
                    base64String: base64String.split(',')[1],
                },
            }));
        };
        reader.readAsDataURL(file);
    };

    const saveChanges = async () => {
        try {
            setIsLoading(true);

            let imageOperation: ProductOperationType | ProductOperationType.Nothing;

            const hasOriginalImage = !!originalProduct.image?.id;
            const hasCurrentImage = !!product.image?.base64String;
            const isImageRemoved = hasOriginalImage && !hasCurrentImage;

            const isImageNew = !hasOriginalImage && hasCurrentImage;
            const isImageUpdated = hasOriginalImage && hasCurrentImage;

            if (isImageRemoved) {
                imageOperation = ProductOperationType.Delete;
            } else if (isImageNew) {
                imageOperation = ProductOperationType.Create;
            } else if (isImageUpdated) {
                imageOperation = ProductOperationType.Update;
            } else {
                imageOperation = ProductOperationType.Nothing;
            }

            console.warn(product);

            const request: UpdateAdminProductRequest = {
                id: product.id,
                name: product.name,
                article: product.article,
                description: product.description,
                status: product.status,
                count: product.count,
                newPrice: product.newPrice,
                oldPrice: product.oldPrice || 0,
                isVisible: product.isVisible,
                imageId: product.image?.id ?? undefined,
                imageName: product.image?.name ?? undefined,
                imageBase64String: product.image?.base64String ?? undefined,
                category: product.category,
                attributes: product.attributes
            };

            if (imageOperation !== undefined) {
                request.type = imageOperation;
            }

            const response = await AdminService.UpdateAdminProduct(request);
            setIsLoading(false);

            if (response.success && response.status === 200) {
                addNotification({ header: 'Товар успешно обновлён' });
                setOriginalProduct(JSON.parse(JSON.stringify(product)));
                setIsEditing(false);
            } else {
                addNotification({ header: 'Ошибка при сохранении' });
            }
        } catch (error) {
            addNotification({ header: 'Ошибка сети' });
            setIsLoading(false);
        }
    };

    if (!product.id) return <div>Загрузка...</div>;

    return (
        <div className={styles.container}>
            <div className={styles.mainProductContainer}>
                <h2 className={styles.h2}>
                    Товар {product.article}
                    {!isEditing ? (
                        <button className={styles.editButton} onClick={startEditing}>
                            Редактировать
                        </button>
                    ) : (
                        <div className={styles.editActions}>
                            <button className={styles.cancelButton} onClick={cancelEditing} disabled={isLoading}>
                                Отменить
                            </button>
                            <button className={styles.saveButton} onClick={saveChanges} disabled={isLoading}>
                                {isLoading ? 'Сохранение...' : 'Сохранить'}
                            </button>
                        </div>
                    )}
                </h2>

                <div className={styles.bodyProduct}>
                    <div className={styles.imageContainer}>
                        <Image isBase64Image={true} base64String={product.image?.base64String} className={styles.image} />
                        {isEditing && (
                            <div className={styles.imageActions}>
                                <input type="file" accept="image/*" onChange={handleImageUpload} />
                                <button type="button" onClick={handleRemoveImage} className={styles.removeImageButton}>
                                    Удалить изображение
                                </button>
                            </div>
                        )}
                    </div>

                    <div className={styles.productContainer}>
                        <ul className={styles.ul}>
                            <li>
                                Название:{' '}
                                {isEditing ? (
                                    <input
                                        type="text"
                                        value={product.name}
                                        onChange={e => handleFieldChange('name', e.target.value)}
                                        className={styles.input}
                                    />
                                ) : (
                                    product.name
                                )}
                            </li>
                            <li>
                                Артикул:{' '}
                                {isEditing ? (
                                    <input
                                        type="text"
                                        value={product.article}
                                        onChange={e => handleFieldChange('article', e.target.value)}
                                        className={styles.input}
                                    />
                                ) : (
                                    product.article
                                )}
                            </li>
                            <li>
                                Категория:{' '}
                                {isEditing ? (
                                    <input
                                        type="text"
                                        value={product.category}
                                        onChange={e => handleFieldChange('category', e.target.value)}
                                        className={styles.input}
                                    />
                                ) : (
                                    product.category
                                )}
                            </li>
                            <li>
                                Описание:{' '}
                                {isEditing ? (
                                    <textarea
                                        value={product.description}
                                        onChange={e => handleFieldChange('description', e.target.value)}
                                        className={styles.textarea}
                                    />
                                ) : (
                                    product.description
                                )}
                            </li>
                            <li>
                                Статус:{' '}
                                {isEditing ? (
                                    <select
                                        value={product.status}
                                        onChange={e => handleFieldChange('status', Number(e.target.value))}
                                        className={styles.select}
                                    >
                                        {Object.keys(ProductStatus)
                                            .filter(key => isNaN(Number(key)))
                                            .map(key => (
                                                <option key={key} value={ProductStatus[key as keyof typeof ProductStatus]}>
                                                    {statusLabels[ProductStatus[key as keyof typeof ProductStatus]]}
                                                </option>
                                            ))}
                                    </select>
                                ) : (
                                    statusLabels[product.status]
                                )}
                            </li>
                            <li>
                                На складе: {isEditing ? (
                                    <input
                                        type="number"
                                        value={product.count}
                                        onChange={e => handleFieldChange('count', Number(e.target.value))}
                                        min="0"
                                        className={styles.input}
                                    />
                                ) : (
                                    `${product.count} шт.`
                                )}
                            </li>
                            <li>
                                Цена со скидкой:{' '}
                                <input
                                    type="number"
                                    value={product.newPrice}
                                    onChange={e => handleFieldChange('newPrice', Number(e.target.value))}
                                    min="0"
                                    step="0.01"
                                    disabled={!isEditing}
                                    className={isEditing ? styles.input : ''}
                                />{' '}
                                руб.
                            </li>
                            <li>
                                Старая цена:{' '}
                                <input
                                    type="number"
                                    value={product.oldPrice || ''}
                                    onChange={e =>
                                        handleFieldChange('oldPrice', e.target.value === '' ? 0 : Number(e.target.value))
                                    }
                                    placeholder="Без скидки"
                                    min="0"
                                    step="0.01"
                                    disabled={!isEditing}
                                    className={isEditing ? styles.input : ''}
                                />{' '}
                                руб.
                            </li>
                            <li>
                                Видимость:{' '}
                                {isEditing ? (
                                    <label className={styles.checkboxLabel}>
                                        <input
                                            type="checkbox"
                                            checked={product.isVisible}
                                            onChange={e => handleFieldChange('isVisible', e.target.checked)}
                                        />
                                        Отображается в каталоге
                                    </label>
                                ) : (
                                    product.isVisible ? 'Да' : 'Нет'
                                )}
                            </li>
                        </ul>
                </div>
            </div>
            <div className={styles.attributesContainer}>
                <h2 className={styles.heading}>Характеристики</h2>
                    {isEditing ? (
                        <div>
                            <AdminProductAttributes
                                attributes={product.attributes}
                                productId={product.id}
                                onChange={(updatedAttributes) =>
                                setProduct(prev => ({ ...prev, attributes: updatedAttributes }))
                                }
                            />
                        </div>
                    ) : (
                    <div>
                        <ProductAttributes attributes={product.attributes} />
                    </div>
                )}
            </div>
                <div className={styles.productFooter}>
                    <div className={styles.priceContainer}>
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
                        <span className={styles.newPrice}>{product.newPrice * count} руб.</span>
                            {product.oldPrice > 0 && (
                                <span className={styles.oldPrice}>{product.oldPrice * count} руб.</span>
                            )}
                    </div>
                    <button className={styles.buyButton} onClick={handleAddToBasket}>
                        В корзину
                    </button>
                </div>
                <div className={styles.rightBar}>
                    <ViewAlreadyExistProductInBasket
                        isOpen={isModalOpen}
                        onClose={() => setIsModalOpen(false)}
                        product={{} as Product}
                        productCount={count}
                        onCountChange={handleProductCountChange}
                    />
                </div>
                {isModalOpen && <div className={styles.overlay} />}
            </div>
        </div>
    );
};