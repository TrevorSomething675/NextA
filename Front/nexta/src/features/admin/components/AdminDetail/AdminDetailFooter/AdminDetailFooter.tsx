import { GetAdminDetailResponse } from "../../../models/GetAdminDetail";
import styles from './AdminDetailFooter.module.css';

const AdminDetailFooter:React.FC<{detail:GetAdminDetailResponse}> = () => {
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