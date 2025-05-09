import Detail from '../../../models/Detail';
import styles from './warehouseDetail.module.css';

const WarehouseDetail:React.FC<{detail:Detail}> = ({detail}) =>{
    return <tr className={styles.tr}>
            <td>{detail.name}</td>
            <td>{detail.article}</td>
            <td>{detail.description}Датчик давления</td>
            <td>{detail.status}</td>
            <td>{detail.deliveryDate}</td>
            <td>{detail.count}</td>
            <td>{detail.newPrice}</td>
            <td>
            <button className={styles.addBasketBtn}>
                В корзину
            </button>
            </td>
        </tr>
}

export default WarehouseDetail;