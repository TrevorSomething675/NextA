import { CategoriesContainer } from '../../../categoeries/components/CategoriesContainer/CategoriesContainer';
import { AdminAddCategory } from '../../components/AdminAddCategory/AdminAddCategory';
import { AdminDeleteCategory } from '../../components/AdminDeleteCategory/AdminDeleteCategory';
import styles from './AdminCategoryPage.module.css';

export const AdminCategoryPage = () => {
    return <div className={styles.container}>
        <h2 className={styles.h2}>Создать категорию</h2>
        <AdminAddCategory />
        <h2 className={styles.h2}>Удалить категорию</h2>
        <div>
            <AdminDeleteCategory />
        </div>
        <h2 className={styles.h2}>Категории</h2>
        <div className={styles.categoriesContainer}>
            <CategoriesContainer />
        </div>
    </div>
}