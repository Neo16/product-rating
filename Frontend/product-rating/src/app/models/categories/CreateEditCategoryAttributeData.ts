import { CreateEditCategoryAttributeValueData } from './CreateEditCategoryAttributeValueData';
import { AttributeType } from './AttributeType';

export class CreateEditCategoryAttributeData
{      
    attributeName: string  = "";

    hasFixedValues: boolean = false;

    type: AttributeType = AttributeType.String;

    attributeId: string | null = null;

    values: CreateEditCategoryAttributeValueData[] = [];
}