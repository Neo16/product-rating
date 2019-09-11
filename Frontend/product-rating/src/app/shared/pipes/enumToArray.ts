import { PipeTransform, Pipe } from '@angular/core';

@Pipe({
    name: 'enumToArray'
})
export class EnumToArrayPipe implements PipeTransform {
    transform(data: Object) {
        var keys =  Object.keys(data).filter(k => typeof data[k as any] === "number");        
        return keys.map(k => data[k as any]);
    }
}