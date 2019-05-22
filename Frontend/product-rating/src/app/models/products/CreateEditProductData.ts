import { CreateEditIntAttribute } from './CreateEditIntAttribute';
import { CreateEditStringAttribute } from './CreateEditStringAttribute';
import { PictureData } from '../PictureData';
import { SimpleDateData } from '../SimpleDateData';

export class CreateEditProductData
    {
        id: string;
        name: string; 

        brandId: string | null = null;   

        categoryId: string | null = null;

        intAttributes: CreateEditIntAttribute[] = [];

        stringAttributes: CreateEditStringAttribute[] = [];

        pictures: PictureData[];

        thumbnailPicture: PictureData = new PictureData();       

        startOfProduction: SimpleDateData = new SimpleDateData();
        endOfProduction: SimpleDateData = new SimpleDateData();
    }