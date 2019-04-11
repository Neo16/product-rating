import { initialState, SearchState } from './search.state';
import { SearchActionTypes, ChangeFilterAction } from './search.actions';

export function searchReducer(state = initialState, action: any):  SearchState {
  switch (action.type) {
    case SearchActionTypes.CHANGE_FILTER: {
      var newFilter = (action as ChangeFilterAction).payload;
      return {
        ...state,
        textFilter: newFilter.textFilter,
        categoryId: newFilter.categoryId,
        brandId: newFilter.brandId,
        minimumPrice: newFilter.minimumPrice,
        maximumPrice: newFilter.maximumPrice,
        orderBy: newFilter.orderBy,
        order: newFilter.order,
        intAttributes: newFilter.intAttributes,
        stringAttributes: newFilter.stringAttributes         
      };
    }
    default: {
      return state;
    }
  }
 }