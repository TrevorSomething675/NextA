import styles from './basketItem.module.css';
import Image from 'next/image'

const BasketItem = () => {
    return <li className={styles.li}>
        <table className={styles.table}>
            <thead className={styles.thead}>
                <tr>
                    <th>Название</th>
                    <th>Артикул</th>
                    <th>Описание</th>
                    <th>Статус</th>
                    <th>Доставка в ПВЗ</th>
                    <th>Кол-во</th>
                    <th>Стоимость</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td className={styles.td}>Henshel</td>
                    <td className={styles.td}>K28600RCL004</td>
                    <td className={styles.td}>Датчик давления</td>
                    <td className={styles.td}>В работе</td>
                    <td className={styles.td}>08.03.2025</td>
                    <td className={styles.td}>1</td>
                    <td className={styles.td}>1000.66</td>
                    <td className={styles.td}>
                    <button className={styles.removeBasketBtn}>
                    <svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" className={styles.trash} viewBox="0 0 16 16">
                        <path d="M6.5 1h3a.5.5 0 0 1 .5.5v1H6v-1a.5.5 0 0 1 .5-.5M11 2.5v-1A1.5 1.5 0 0 0 9.5 0h-3A1.5 1.5 0 0 0 5 1.5v1H1.5a.5.5 0 0 0 0 1h.538l.853 10.66A2 2 0 0 0 4.885 16h6.23a2 2 0 0 0 1.994-1.84l.853-10.66h.538a.5.5 0 0 0 0-1zm1.958 1-.846 10.58a1 1 0 0 1-.997.92h-6.23a1 1 0 0 1-.997-.92L3.042 3.5zm-7.487 1a.5.5 0 0 1 .528.47l.5 8.5a.5.5 0 0 1-.998.06L5 5.03a.5.5 0 0 1 .47-.53Zm5.058 0a.5.5 0 0 1 .47.53l-.5 8.5a.5.5 0 1 1-.998-.06l.5-8.5a.5.5 0 0 1 .528-.47M8 4.5a.5.5 0 0 1 .5.5v8.5a.5.5 0 0 1-1 0V5a.5.5 0 0 1 .5-.5"/>
                    </svg>
                    </button>
                    </td>
                </tr>
                <tr>
                    <td className={styles.td}>Henshel</td>
                    <td className={styles.td}>K28600RCL004</td>
                    <td className={styles.td}>Датчик давления</td>
                    <td className={styles.td}>Выкуплен</td>
                    <td className={styles.td}>08.03.2025</td>
                    <td className={styles.td}>1</td>
                    <td className={styles.td}>1000.66</td>
                    <td className={styles.td}>
                    <button className={styles.removeBasketBtn}>
                    <svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" className={styles.trash} viewBox="0 0 16 16">
                        <path d="M6.5 1h3a.5.5 0 0 1 .5.5v1H6v-1a.5.5 0 0 1 .5-.5M11 2.5v-1A1.5 1.5 0 0 0 9.5 0h-3A1.5 1.5 0 0 0 5 1.5v1H1.5a.5.5 0 0 0 0 1h.538l.853 10.66A2 2 0 0 0 4.885 16h6.23a2 2 0 0 0 1.994-1.84l.853-10.66h.538a.5.5 0 0 0 0-1zm1.958 1-.846 10.58a1 1 0 0 1-.997.92h-6.23a1 1 0 0 1-.997-.92L3.042 3.5zm-7.487 1a.5.5 0 0 1 .528.47l.5 8.5a.5.5 0 0 1-.998.06L5 5.03a.5.5 0 0 1 .47-.53Zm5.058 0a.5.5 0 0 1 .47.53l-.5 8.5a.5.5 0 1 1-.998-.06l.5-8.5a.5.5 0 0 1 .528-.47M8 4.5a.5.5 0 0 1 .5.5v8.5a.5.5 0 0 1-1 0V5a.5.5 0 0 1 .5-.5"/>
                    </svg>
                    </button>
                    </td>
                </tr>
                <tr>
                    <td className={styles.td}>Henshel</td>
                    <td className={styles.td}>K28600RCL004</td>
                    <td className={styles.td}>Датчик давления</td>
                    <td className={styles.td}>На складе</td>
                    <td className={styles.td}>08.03.2025</td>
                    <td className={styles.td}>1</td>
                    <td className={styles.td}>1000.66</td>
                    <td className={styles.td}>
                    <button className={styles.removeBasketBtn}>
                    <svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" className={styles.trash} viewBox="0 0 16 16">
                        <path d="M6.5 1h3a.5.5 0 0 1 .5.5v1H6v-1a.5.5 0 0 1 .5-.5M11 2.5v-1A1.5 1.5 0 0 0 9.5 0h-3A1.5 1.5 0 0 0 5 1.5v1H1.5a.5.5 0 0 0 0 1h.538l.853 10.66A2 2 0 0 0 4.885 16h6.23a2 2 0 0 0 1.994-1.84l.853-10.66h.538a.5.5 0 0 0 0-1zm1.958 1-.846 10.58a1 1 0 0 1-.997.92h-6.23a1 1 0 0 1-.997-.92L3.042 3.5zm-7.487 1a.5.5 0 0 1 .528.47l.5 8.5a.5.5 0 0 1-.998.06L5 5.03a.5.5 0 0 1 .47-.53Zm5.058 0a.5.5 0 0 1 .47.53l-.5 8.5a.5.5 0 1 1-.998-.06l.5-8.5a.5.5 0 0 1 .528-.47M8 4.5a.5.5 0 0 1 .5.5v8.5a.5.5 0 0 1-1 0V5a.5.5 0 0 1 .5-.5"/>
                    </svg>
                    </button>
                    </td>
                </tr>
            </tbody>
        </table>
    </li>
}

export default BasketItem;

/*
<Image src='/newsItem1.jpg' height={100} width={100} alt='' className={styles.image}/>

            <div className={styles.detailContainer}>
                <h2 className={styles.detailHeader}>Название детали</h2>
                <div className={styles.detailBody}>
                <p className={styles.p}>Описание деталиОписание деталиОписание деталиОписание деталиОписание деталиОписание деталиОписание детали
                    Описание деталиОписание деталиОписание деталиОписание деталиОписание деталиОписание деталиОписание 
                    деталиОписание деталиОписание детали</p>
                </div>
                <div className={styles.detailFooter}>
                    <p className={styles.priceText}>
                        Цена: 660.03 руб.
                    </p>
                </div>
            </div>
            */