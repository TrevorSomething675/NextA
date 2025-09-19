import { create } from "zustand";
import { Category } from "../models/Category";

type CategoriesState = {
    categories:Category[];
}

type CategoriesStateActions = {
    setCategories: (categories: Category[]) => void;
}

export type CategoriesStore = CategoriesState & CategoriesStateActions;

export const useCategoriesStore = create<CategoriesStore>((set) => ({
    categories: [],
    setCategories: (newcategories) => set({categories: newcategories})
}));