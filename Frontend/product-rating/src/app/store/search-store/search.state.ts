import { SearchParams } from 'src/app/models/SearchParams';
import { ProductCellData } from 'src/app/models/ProductCellData';
import { CategoryHeader } from 'src/app/models/CategoryHeader';
import { BrandHeader } from 'src/app/models/BrandHeader';
import { ProductOrder } from 'src/app/models/ProductOrder';
import { Order } from 'src/app/models/Order';

export interface SearchState { 
   filter: SearchParams,
   products: ProductCellData[],
   categories: CategoryHeader[],
   brands: BrandHeader[],
   maxPrice: number | null,
}

export const initialState: SearchState = {
   filter: {
      textFilter: null,
      categoryId:  null,
      brandIds: [],
      minimumPrice: 0,
      maximumPrice:  1000,
      orderBy: ProductOrder.BestScore,
      order: Order.Desc,
      intAttributes: [],
      stringAttributes: [],
   } as SearchParams,
   products: [],
   categories: [],
   brands: [],
   maxPrice: 1000,
};



