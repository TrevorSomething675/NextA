import { Product } from "../../models/Product"
import { create } from 'zustand';

type ProductsState = {
    products:Product[];
    searchTerm:string;
    totalPageCount: number;
    category:string;
    priceFilters: { min: number; max: number; range: string | null };
    page:number;
    isPriceRangeImmediate: boolean;
    clearImmediatePriceUpdate: () => void
}

type ProductActions = {
    setCategory: (category:string) => void;
    setProducts: (products: Product[]) => void;
    setSearchTerm: (searchTerm:string) => void;
    setTotalPageCount: (totalPageCount: number) => void;
    setPriceFilters: (filters: { min: number; max: number; range: string | null }) => void;
    setPage: (page:number) => void;
    setImmediatePriceUpdate: () => void;
    clearImmediatePriceUpdate: () => void;
}

export type SearchProductsStore = ProductsState & ProductActions;

export const useSearchProductsStore = create<SearchProductsStore>((set) => ({
    category: '',
    products: [],
    searchTerm: '',
    totalPageCount: 1,
    priceFilters: { min: 1, max: 1000000, range: 'any' },
    page: 1,
    isPriceRangeImmediate: false,

    setCategory: (newCategory) => set({category: newCategory}),
    setTotalPageCount: (newTotalPageCount) => set({totalPageCount: newTotalPageCount}),
    setProducts: (products) => set({products}),
    setSearchTerm: (newSearchTerm) => set({searchTerm: newSearchTerm}),
    setPriceFilters: (filters) => set({ priceFilters: filters }),
    setPage: (newPage) => set({page: newPage}),
    setImmediatePriceUpdate: () => set({ isPriceRangeImmediate: true }),
    clearImmediatePriceUpdate: () => set({ isPriceRangeImmediate: false })
}));