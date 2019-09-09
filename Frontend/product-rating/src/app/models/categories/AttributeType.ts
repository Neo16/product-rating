export enum AttributeType {
        String = 1,
        Int = 2
}

export const AttributeTypeDisplay: { [index: number]: string } = {};

AttributeTypeDisplay[AttributeType.String] = "String";
AttributeTypeDisplay[AttributeType.Int] = "Int";