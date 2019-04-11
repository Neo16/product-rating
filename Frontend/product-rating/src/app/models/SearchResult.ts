import { ProductCellData } from './ProductCellData';
import { CategoryHeader } from './CategoryHeader';
import { BrandHeader } from './BrandHeader';

export interface SearchResult
{
    products: ProductCellData[];

    categories: CategoryHeader[];

    brands: BrandHeader[];
}