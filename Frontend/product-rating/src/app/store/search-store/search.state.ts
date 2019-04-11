import { SearchParams } from 'src/app/models/SearchParams';
import { ProductCellData } from 'src/app/models/ProductCellData';
import { CategoryHeader } from 'src/app/models/CategoryHeader';
import { BrandHeader } from 'src/app/models/BrandHeader';

export interface SearchState { 
   filter: SearchParams,
   products: ProductCellData[],
   categories: CategoryHeader[],
   brands: BrandHeader[] 
}

export const initialState: SearchState = {
   filter: new SearchParams(),
   products: [],
   categories: [],
   brands: [] 
};



