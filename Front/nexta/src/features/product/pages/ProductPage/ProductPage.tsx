import ProductsService from "../../../../services/ProductService";
import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import styles from './ProductPage.module.css';
import { Product, ProductStatus } from "../../../../models/Product";
import { useNotifications } from "../../../../shared/components/Notifications/Notifications";
import Image from "../../../../shared/components/Image/Image";
import { ViewAlreadyExistProductInBasket } from "../../../../shared/components/ViewAlreadyExistProductInBasket/ViewAlreadyExistProductInBasket";
import { AddBasketProductRequest } from "../../../../http/models/basketProduct/AddBasketProduct";
import authStore from "../../../../stores/AuthStore/authStore";
import BasketService from "../../../../services/BasketService";
import basket from "../../../../stores/basket";

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
                const response = await ProductsService.GetById(id);
                if(response.success && response.status === 200){
                    setProduct(response.data.product);
                }
            }
        }
        fetch();
    }, [id]);

    const increment = () => {
        setCount(count => count + 1);
    };

    const decrement = () => {
        setCount(count => Math.max(1, count - 1));
    };
        
    const handleProductCountChange = (newCount: number) => {
        setCount(newCount);
    };

    const handleAddToBasket = async () => {
        const request:AddBasketProductRequest = {
            userId: authStore?.user?.id ?? '',
            productId: product.id,
            countToPay: count
        };

        const response = await BasketService.AddBasketProduct(request);
        if (response.success && response.status === 200) {
            addNotification({
                header: `Товар ${response.data.basketProduct.name} добавлен в корзину`
            });
            basket.addBasketProduct(response.data.basketProduct)
        } else if (!response.success && response.status === 409){
            setIsModalOpen(true);
        } 
    };
    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const value = parseInt(e.target.value, 10);
        
        if (!isNaN(value) && value >= 1) {
            setCount(value);
        } else {
            setCount(1);
        }
    };

        
    if(product) {
        return <div className={styles.container}>
            <h2 className={styles.h2}>
                Товар {product?.article}
            </h2>

            <div className={styles.bodyProduct}>
                <div className={styles.imageContainer}>
                    <Image isBase64Image={true} base64String={product?.image?.base64String} className={styles.image} />
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
                                </span>
                                {(product.oldPrice !== undefined && product.oldPrice != 0) &&
                                    <span className={styles.oldPrice}>
                                        {product.oldPrice * count} руб.
                                    </span>
                                }
                            </div>
                            <button className={styles.buyButton} onClick={handleAddToBasket}>
                                В корзину
                            </button>
                        </div>
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