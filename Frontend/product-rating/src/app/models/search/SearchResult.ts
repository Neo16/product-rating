import { CategoryHeader } from './CategoryHeader';
import { BrandHeader } from './BrandHeader';
import { ProductCellData } from '../products/ProductCellData';

export interface SearchResult
{
    products: ProductCellData[];

    categories: CategoryHeader[];

    brands: BrandHeader[];

    maxPriceOption: number;

    totalNumOfResults: number;
}