import { AttributeType } from '../AttributeType';
import { SearchCategoryAttributeValueData } from './SearchCategoryAttributeValueData';

export class SearchCategoryAttributeData
{
    attributeId: string;

    attributeName: string; 

    hasFixedValues: boolean;

    type: AttributeType;

    values: SearchCategoryAttributeValueData[] = [];
}