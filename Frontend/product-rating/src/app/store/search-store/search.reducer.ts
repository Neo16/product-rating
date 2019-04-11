import { initialState, SearchState } from './search.state';
import { SearchActionTypes, ChangeFilterAction, SearchSuccessAction } from './search.actions';

export function searchReducer(state: SearchState = initialState, action: any):  SearchState {
  switch (action.type) {
    case SearchActionTypes.CHANGE_FILTER: {
      var newFilter = (action as ChangeFilterAction).payload;
      return {
        ...state,
        filter: newFilter
      };
    }      
    case SearchActionTypes.SEARCH_SUCCESS: {
      var result = (action as SearchSuccessAction).payload;
      return {
        ...state,
        products: result.products,
        categories: result.categories,
        brands: result.brands
      };
    }  
    default: {
      return state;
    } 
   
  }
 }