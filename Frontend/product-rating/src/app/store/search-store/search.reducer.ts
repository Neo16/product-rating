import { initialState, SearchState } from './search.state';
import { SearchActionTypes, ChangeFilterAction, SearchSuccessAction,
AddCategoryFilterAction, RemoveCategoryFilterAction } from './search.actions';
import { filter } from 'rxjs/operators';

export function searchReducer(state: SearchState = initialState, action: any):  SearchState {
  switch (action.type) {
    case SearchActionTypes.ADD_CATEGORY_FILTER: {
      var categoryId = (action as AddCategoryFilterAction).payload;
      return  {
        ...state,
        filter: {
            ...state.filter,
            categoryId: categoryId
        }
      }           
    }      
    case SearchActionTypes.REMOVE_CATEGORY_FILTER: {        
     var newState = {
          ...state,    
     };
     
     newState.filter.categoryId = null;
     return newState;   
    }      
    case SearchActionTypes.CHANGE_FILTER: {
      var newFilter = (action as ChangeFilterAction).payload;
      return {
        ...state,
        filter: Object.assign(state.filter, newFilter)       
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