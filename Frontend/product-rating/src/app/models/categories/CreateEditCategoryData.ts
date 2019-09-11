import { CreateEditCategoryAttributeData } from './CreateEditCategoryAttributeData';

export class CreateEditCategoryData
{
    name: string;

    parentCategoryId: string | null;

    attributes: CreateEditCategoryAttributeData[] = [];

    thumbnailPictureId: string | null;
}