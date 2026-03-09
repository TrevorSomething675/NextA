import { CategoryApi } from "../../../shared/http/category/categoryApi"
import { useCategoriesStore } from "../../../shared/stores/categories/categoriesStore";

export const useGetCategories = () => {
    const { setCategories } = useCategoriesStore();

    const getCategories = async() => {
        const response = await CategoryApi.Get();

        if(response.success && response.status === 200){
            setCategories(response.data);
        }
        
        return {
            success: response.success,
            status: response.status
        }
    }

    return { getCategories }
}