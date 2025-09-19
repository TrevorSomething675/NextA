import { Product } from "../../models/Product"
import { create } from 'zustand';

type ProductsState = {
    products:Product[];
    searchTerm:string;
    totalPageCount: number;
    category:string
}

type ProductActions = {
    setCategory: (category:string) => void;
    setProducts: (products: Product[]) => void;
    setSearchTerm: (searchTerm:string) => void;
    setTotalPageCount: (totalPageCount: number) => void;
}

export type SearchProductsStore = ProductsState & ProductActions;

export const useSearchProductsStore = create<SearchProductsStore>((set) => ({
    category: '',
    products: [],
    searchTerm: '',
    totalPageCount: 1,

    setCategory: (newCategory) => set({category: newCategory}),
    setTotalPageCount: (newTotalPageCount) => set({totalPageCount: newTotalPageCount}),
    setProducts: (products) => set({products}),
    setSearchTerm: (newSearchTerm) => set({searchTerm: newSearchTerm}),
}));