import { ProductOrder } from './ProductOrder';
import { Order } from './Order';
import { DisplayIntAttribute } from '../DisplayIntAttribute';
import { DisplayStringAttribute } from '../DisplayStringAttribute';

export class SearchParams {  
    textFilter: string | null;
    categoryId: string | null;
    brandIds: string[];
    minimumPrice: number | null;
    maximumPrice: number | null;   
    orderBy: ProductOrder | null;
    order: Order | null;
    intAttributes: DisplayIntAttribute[];
    stringAttributes: DisplayStringAttribute[];
 }