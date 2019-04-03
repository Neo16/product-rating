import { ProductOrder } from './ProductOrder';

export enum Order {
    Asc = 1,
    Desc = 2  
}

export const OrderDisplay: { [index: number]: string } = {};

OrderDisplay[Order.Asc] = "Ascending";
OrderDisplay[Order.Desc] = "Descending";
