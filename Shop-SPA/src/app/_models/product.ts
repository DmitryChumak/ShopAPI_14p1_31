import { Category } from './category';

export interface Product {
    id: number;
    name: string;
    goodCount: number;
    categoryId: number;
    category?: Category;
}
