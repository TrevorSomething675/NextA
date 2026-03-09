import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import styles from './ProductPage.module.css';
import { useNotifications } from "../../../shared/contexts/notifications/NotificationsContext";
import { ViewAlreadyExistProductInBasket } from "../../../widgets/ui/basket/viewAlreadyExistProductInBasket/ViewAlreadyExistProductInBasket";
import { ProductApi } from "../../../shared/http/product/productApi";
import { BasketApi } from "../../../shared/http/basket/basketApi";
import authStore from "../../../shared/stores/auth/authStore";
import basketStore from "../../../shared/stores/basket/basketStore";
import { Image } from "../../../shared/ui";
import { Product, ProductStatus } from "../../../entities/product/product";
import { ProductAttributes } from "../../../widgets/ui/product-attributes/ProductAttributes";

const statusLabels = {
    [ProductStatus.Unknown]: 'Неизвестный статус',
    [ProductStatus.InStock]: 'Есть на складе',
    [ProductStatus.OutOfStock]: 'Нет на складе',
};

export const ProductPage = () => {
    const {id} = useParams();
    const [product, setProduct] = useState({} as Product)
    const [count, setCount] = useState(1);
    const { addNotification } = useNotifications();
    const [isModalOpen, setIsModalOpen] = useState(false);

    useEffect(() => {
        const fetch = async() =>{
            if(id !== undefined){
                const response = await ProductApi.GetById(id);
                if(response.success && response.status === 200){
                    setProduct(response.data);
                }
            }
        }
        fetch();
    }, [id]);
    
    const increment = () => {
        if(count < 10){
            setCount(count => count + 1);
        }
    };

    const decrement = () => {
        if(count > 0){
            setCount(count => Math.max(1, count - 1));
        }
    };
        
    const handleProductCountChange = (newCount: number) => {
        setCount(newCount);
    };

    const handleAddToBasket = async () => {
        const userId = authStore?.user?.id ?? '';
        const productId = product.id;
        const countToPay = count;

        const response = await BasketApi.AddProductToBasket(userId, productId, countToPay);
        if (response.success && response.status === 200) {
            addNotification({
                header: 'Товар добавлен в корзину'
            });
            basketStore.addBasketProduct(response.data);
        } else if (!response.success && response.status === 409){
            setIsModalOpen(true);
        }
    };
    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const value = parseInt(e.target.value, 10);
        
        if (!isNaN(value) && value >= 1 && value <= 10) {
            setCount(value);
        } else {
            setCount(1);
        }
    };
    
    if(product) {
        return <div className={styles.container}>
            <div className={styles.cardContainer}>
                <h2 className={styles.h2}>
                    Товар {product?.article}
                </h2>
                <div className={styles.headerProduct}>
                    <div className={styles.imageContainer}>
                        <Image isBase64Image={true} base64String={product?.images?.[0]?.base64String} className={styles.image} />
                    </div>
                    <div className={styles.productContainer}>
                        <ul className={styles.ul}>
                            <li> - {product.name}</li>
                            <li> - {product.description}</li>
                            <li> - {statusLabels[product.status]}</li>
                            <li> - Осталось на складе: {product.count}</li>
                        </ul>
                    </div>
                </div>
                <div>
                    {product.attributes && product?.attributes?.length > 0 && <div>
                        <h3 className={styles.h3}>Характеристики</h3>
                        <div className={styles.productAttributes}>
                            <ProductAttributes attributes={product.attributes} />
                        </div>
                    </div>}
                </div>
                <div className={styles.productFooter}>
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
                                {product.newPrice * count} руб.
                            </span>                            {(product.oldPrice !== undefined && product.oldPrice != 0) &&
                                <span className={styles.oldPrice}>
                                    {product.oldPrice * count} руб.
                                </span>
                            }
                        </div>
                    </div>
                    <div>
                        <button className={styles.buyButton} onClick={handleAddToBasket}>
                            В корзину
                        </button>
                    </div>
                </div>
                <div className={styles.rightBar}>
                    <ViewAlreadyExistProductInBasket
                        isOpen={isModalOpen}
                        onClose={() => setIsModalOpen(false)}
                        product={product}
                        productCount={count}
                        onCountChange={handleProductCountChange}
                    />
                </div>
                {isModalOpen && <div className={styles.overlay} />}
            </div>
        </div>
    }
}