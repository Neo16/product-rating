import { CreateEditIntAttribute } from './CreateEditIntAttribute';
import { CreateEditStringAttribute } from './CreateEditStringAttribute';

export class CreateEditProductData
    {
        id: string;
        name: string; 

        brandId: string;   

        categoryId: string;

        intAttributes: CreateEditIntAttribute[];

        stringAttributes: CreateEditStringAttribute[];

        pictureIds: string[];

        thumbnailPictureId: string | null;

        thumbnailPictureString: string;

        startOfProduction: Date;
        endOfProduction: Date;
    }