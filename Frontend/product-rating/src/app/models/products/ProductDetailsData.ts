import { Attribute } from '../Attribute';

export interface ProductDetailsData
{
    id: string;
    name: string;

    brandName: string;
    brandId: string;

    categoryName: string;
    categoryId: string;

    attributes: Attribute[];    
    pictures: string[];

    startOfProduction: Date | string;
    endOfProduction: Date | string;

    scoreValue: number;
}