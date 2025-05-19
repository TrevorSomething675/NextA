import { useState } from 'react';
import Detail, { DetailStatus } from '../../../models/Detail';
import styles from './detailBody.module.css';

interface Props{
    detail: Detail;
}
const statusLabels = {
    [DetailStatus.Unkown]: 'Неизвестный статус',
    [DetailStatus.InStock]: 'Есть на складе',
    [DetailStatus.OutOfStock]: 'Нет на складе',
};

const DetailBody:React.FC<Props> = ({detail}) => {
    const [count, setCount] = useState(1);

    const increment = () => {
      setCount(count => count + 1);
    };
  
    const decrement = () => {
      setCount(count => count - 1);
    };
    
    return <div className={styles.container}>
        <div className={styles.imageContainer}>
            <img src="/defaultImage.jpg" className={styles.image}/>
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
                        <span className={styles.newPrice}>
                            {detail.newPrice * count} руб.
                        </span>
                        {(detail.oldPrice !== undefined && detail.oldPrice != 0) &&
                            <span className={styles.oldPrice}>
                                {detail.oldPrice * count} руб.
                            </span>
                        }
                        <button type="button" className={styles.down} onClick={decrement}>◄</button>
                        <input
                            value={count}
                            type="number"
                            name="quantity"
                            min="1"
                            max="10"
                            step="1"
                            className={styles.countInput}
                            onChange={(e) => setCount(Number(e.target.value))}
                            />
                        <button type="button" className={styles.up} onClick={increment}>►</button>
                    </div>
                    <button className={styles.buyButton}>
                        Заказать
                    </button>
                </div>
            </div>
        </div>
    </div>
}

export default DetailBody;