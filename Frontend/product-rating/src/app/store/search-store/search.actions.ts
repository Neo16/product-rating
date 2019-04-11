import { Action } from '@ngrx/store';
import { SearchParams } from 'src/app/models/SearchParams';

export enum SearchActionTypes {
    CHANGE_FILTER = '[Search] Change Filter'  
  }

  export class ChangeFilterAction implements Action {
    readonly type = SearchActionTypes.CHANGE_FILTER;
    constructor(public payload: SearchParams) {}
}