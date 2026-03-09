import { useGetCategories } from "../../../features/category/getAdminCategories/useGetAdminCategories";
import { useCategoriesStore } from "../../../shared/stores/categories/categoriesStore";
import styles from './CategoriesContainer.module.css';
import { useEffect } from "react";

export const CategoriesContainer = () => {
    const { getCategories } = useGetCategories();
    const { categories } = useCategoriesStore();

    const fetchData = async() => {
        await getCategories();
    }

    useEffect(() => {
        fetchData();
    }, []);

    return <div className={styles.container}>
        {categories && categories.length > 0 && 
        <div>
            {categories.map(category => 
                <div>{category.name}</div>
            )}
        </div>}
    </div>
}