import Detail from '../../models/Detail';
import styles from './detailItem.module.css';

const DetailItem:React.FC<{detail:Detail}> = ({detail}) =>{
    return <li className={styles.li}>
        {detail.id}
    </li>
}

export default DetailItem;