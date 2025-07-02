import { AdminDetail } from '../../../models/AdminDetail';
import styles from './AdminDetailFooter.module.css';

const AdminDetailFooter:React.FC<{detail:AdminDetail}> = () => {
    return <div className={styles.container}>
        <button>
            Вернуться
        </button>
        <button>
            Заказать
        </button>
    </div>
}

export default AdminDetailFooter;