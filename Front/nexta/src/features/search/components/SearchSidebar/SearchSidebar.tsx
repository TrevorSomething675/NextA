import Button from '../../../../shared/components/Button/Button';
import { useCategoriesStore } from '../../../../stores/categoriesStore';
import { useSearchProductsStore } from '../../../../stores/SearchProductsStore/searchProductsStore';
import { SidebarPriceFilter } from '../SidebarPriceFilter/SidebarPriceFilter';
import styles from './SearchSidebar.module.css';
import ProductsService from '../../../../services/ProductService';

export const SearchSidebar = () => {
    const { setCategory, setPriceFilters, setProducts, setPage, setImmediatePriceUpdate} = useSearchProductsStore();
    const { categories } = useCategoriesStore();

    const fetchData = async(query:string, category:string, minPrice?:number, maxPrice?:number) => {
        const response = await ProductsService.Get(query, category, 9, undefined, false, minPrice, maxPrice);
        if(response.success && response.status === 200){
            setProducts(response.data.data.items);
        }
    }

    const handleResetFilters = async() => {
        setCategory('');
        setPriceFilters({ min: 1, max: 1000000, range: "any" });
        setPage(1);
        setImmediatePriceUpdate();
    };

    const handleSearchOnCategory = async(category:string) => {
        setCategory(category);
        await fetchData('', category);
    }

    return <div className={styles.container}>
        <h2 className={styles.firstH2}>Категории</h2>
        <div className={styles.categoryItems}>
            {categories.map(category => 
                <button key={category.name} className={styles.categoryItem} onClick={() => handleSearchOnCategory(category.name)}>
                    {category.name}
                </button>)}
        </div>
        <div>
            <h2 className={styles.h2}>Цена</h2>
            <SidebarPriceFilter />
        </div>
        <Button content='Сбросить фильтр' 
            className={styles.resetFilter} 
            onClick={handleResetFilters}/>
    </div>
}