import { useState } from 'react';
import Detail, { DetailStatus } from '../../../models/Detail';
import styles from './detailBody.module.css';
import Image from '../../image/Image';

interface Props{
    detail: Detail;
}
const statusLabels = {
    [DetailStatus.Unknown]: 'Неизвестный статус',
    [DetailStatus.InStock]: 'Есть на складе',
    [DetailStatus.OutOfStock]: 'Нет на складе',
};

const DetailBody:React.FC<Props> = ({detail}) => {
    const [count, setCount] = useState(1);

    const increment = () => {
        setCount(count => count + 1);
    };

    const decrement = () => {
        setCount(count => Math.max(1, count - 1));
    };
    
    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const value = parseInt(e.target.value, 10);
        
        if (!isNaN(value) && value >= 1) {
            setCount(value);
        } else {
            setCount(1);
        }
    };

    return <div className={styles.container}>
        <div className={styles.imageContainer}>
            <Image isBase64Image={true} base64String={detail.image.base64String} className={styles.image} />
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
}

export default DetailBody;