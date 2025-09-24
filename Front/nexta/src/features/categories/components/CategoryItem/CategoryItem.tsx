import { Category } from '../../../../models/Category';
import styles from './CategoryItem.module.css';

export const CategoryItem:React.FC<{category:Category}> = ({category}) => {
    return <div className={styles.container}>
        {category.name}
    </div>
}