import { AttributeType } from './AttributeType';

export class CreateEditCategoryAttributeValueData {
    type: AttributeType;

    intValue: number 

    stringValue: string 

    valueId: string | null;

    editable: boolean = true;
}