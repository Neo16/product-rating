import { Order } from 'src/app/models/Order';
import { ProductOrder } from 'src/app/models/ProductOrder';
import { IntAttribute } from 'src/app/models/IntAttribute';
import { StringAttribute } from 'src/app/models/StringAttribute';

export interface SearchState {  
   categoryId: string | null;
   brandId: string | null;
   minimumPrice: number | null,
   maximumPrice: number | null,   
   orderBy: ProductOrder | null
   order: Order | null,  
   intAttributes: IntAttribute[];
   stringAttributes: StringAttribute[];
}

export const initialState: SearchState = {
   categoryId: null,
   brandId: null,
   minimumPrice: 0,
   maximumPrice: null,
   orderBy: ProductOrder.BestScore, 
   order: Order.Desc,
   intAttributes: [],
   stringAttributes : []
};



