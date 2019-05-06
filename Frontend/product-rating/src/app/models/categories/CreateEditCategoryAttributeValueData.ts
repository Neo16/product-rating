import { AttributeType } from './AttributeType';

export class CreateEditCategoryAttributeValueData {
    type: AttributeType;

    get intValue(): number {
        return parseInt(this.value);
    }

    get stringValue(): string {
        return this.value;
    }

    valueId: string | null;

    value: string;    
}