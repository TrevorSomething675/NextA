import { useEffect, useState } from 'react';
import styles from './CategoriesContainer.module.css';
import { Category } from '../../../../models/Category';
import CategoryService from '../../../../services/CategoryService';
import { useNotifications } from '../../../../shared/components/Notifications/Notifications';
import { CategoryItem } from '../Category/CategoryItem';

export const CategoriesContainer = () => {
    const {addNotification} = useNotifications();
    const [categories, setCategories] = useState<Category[]>([]);

    const fetch = async() => {
        const response = await CategoryService.Get();
        if(response.success && response.status === 200){
            setCategories(response.data.categories);
        } else{
            addNotification({
                header: 'Возникла ошибка'
            })
        }
    }

    useEffect(() => {
        fetch();
    }, []);

    return <div className={styles.container}>
        {categories && categories.length > 0 && 
        <div>
            {categories.map(category => 
                <CategoryItem key={category.name} category={category}/>
            )}
        </div>}
    </div>
}