export interface CategoryHeader
{
    id: string;
    name: string;        
    numOfProducts: number;   
    subcategories: CategoryHeader[];
    isActive: boolean;
}