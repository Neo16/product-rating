import { DisplayAttribute } from '../DisplayAttribute';

export interface ProductDetailsData
{
    id: string;
    name: string;

    brandName: string;
    brandId: string;

    categoryName: string;
    categoryId: string;

    attributes: DisplayAttribute[];    
    pictures: string[];

    startOfProduction: Date | string;
    endOfProduction: Date | string;

    scoreValue: number;
    scoreByMe: number | null;
}