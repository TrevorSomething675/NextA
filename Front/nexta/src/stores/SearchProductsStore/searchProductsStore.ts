import { Product } from "../../models/Product"
import { create } from 'zustand';

type ProductsState = {
    products:Product[];
    searchTerm:string;
    totalPageCount: number;
}

type ProductActions = {
    setProducts: (products: Product[]) => void;
    setSearchTerm: (searchTerm:string) => void;
    setTotalPageCount: (totalPageCount: number) => void;
}

export type SearchProductsStore = ProductsState & ProductActions;

export const useSearchProductsStore = create<SearchProductsStore>((set) => ({
    products: [],
    searchTerm: '',
    totalPageCount: 1,

    setTotalPageCount: (newTotalPageCount) => set({totalPageCount: newTotalPageCount}),
    setProducts: (products) => set({products}),
    setSearchTerm: (newSearchTerm) => set({searchTerm: newSearchTerm}),
}));