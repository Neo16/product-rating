import { ProductOrder } from './ProductOrder';
import { Order } from './Order';
import { IntAttribute } from './IntAttribute';
import { StringAttribute } from './StringAttribute';

export class SearchParams {  
    textFilter: string | null;
    categoryId: string | null;
    brandId: string | null;
    minimumPrice: number | null;
    maximumPrice: number | null;   
    orderBy: ProductOrder | null;
    order: Order | null;
    intAttributes: IntAttribute[];
    stringAttributes: StringAttribute[];
 }