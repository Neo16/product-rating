import { CreateEditIntAttribute } from './CreateEditIntAttribute';
import { CreateEditStringAttribute } from './CreateEditStringAttribute';
import { PictureData } from '../PictureData';

export class CreateEditProductData
    {
        id: string;
        name: string; 

        brandId: string | null = null;   

        categoryId: string | null = null;

        intAttributes: CreateEditIntAttribute[];

        stringAttributes: CreateEditStringAttribute[];

        pictures: PictureData[];

        thumbnailPicture: PictureData;       

        startOfProduction: Date;
        endOfProduction: Date;
    }