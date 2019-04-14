import { Action } from '@ngrx/store';
import { SearchParams } from 'src/app/models/SearchParams';
import { SearchResult } from 'src/app/models/SearchResult';

export enum SearchActionTypes {
    REMOVE_CATEGORY_FILTER = '[Search] Add category filter',
    ADD_CATEGORY_FILTER = '[Search] Remove category filter',
    CHANGE_FILTER = '[Search] Change Filter',
    FIRE_SERACH = '[Search] Fire Search',
    SEARCH_SUCCESS = '[Search] Success',
    SEARCH_FAILURE = '[Search] Failure'
}

export class AddCategoryFilterAction implements Action {
  readonly type = SearchActionTypes.ADD_CATEGORY_FILTER;
  constructor(public payload: string) {}
}

export class RemoveCategoryFilterAction implements Action {
  readonly type = SearchActionTypes.REMOVE_CATEGORY_FILTER; 
  constructor(public payload: string) {}
}

export class ChangeFilterAction implements Action {
    readonly type = SearchActionTypes.CHANGE_FILTER;
    constructor(public payload: SearchParams) {}
}

export class FireSearchAction implements Action {
    readonly type = SearchActionTypes.FIRE_SERACH;    
}

export class SearchSuccessAction implements Action {
  readonly type = SearchActionTypes.SEARCH_SUCCESS;
  constructor(public payload: SearchResult) {}
}

export class SearchFailureAction implements Action {
  readonly type = SearchActionTypes.SEARCH_FAILURE;
  constructor(public payload: any) {}
}

