import { initialState, SearchState } from './search.state';
import {
  SearchActionTypes, ChangeFilterAction, SearchSuccessAction,
  AddCategoryFilterAction, RemoveCategoryFilterAction
} from './search.actions';
import { filter } from 'rxjs/operators';
import { CategoryHeader } from 'src/app/models/CategoryHeader';

function forAllCategoryInTree(categories: CategoryHeader[], func: (x: CategoryHeader) => void) {
  if (categories == undefined)
    return;
  categories.forEach(cat => {
    func(cat);
    forAllCategoryInTree(cat.subcategories, func);
  })
}

export function searchReducer(state: SearchState = initialState, action: any): SearchState {
  switch (action.type) {
    case SearchActionTypes.ADD_CATEGORY_FILTER: {
      var categoryId = (action as AddCategoryFilterAction).payload;
      var newState = {
        ...state,
        filter: {
          ...state.filter,
          categoryId: categoryId
        }
      }
      //make the selected cat. active on ui 
      forAllCategoryInTree(newState.categories, (c: CategoryHeader) => {
        c.isActive = c.id === categoryId;
      });
      return newState;
    }
    case SearchActionTypes.REMOVE_CATEGORY_FILTER: {
      var categoryId = (action as RemoveCategoryFilterAction).payload;
      var newState = {
        ...state,
      };

      //Remove subcategories and make it unselected 
      forAllCategoryInTree(newState.categories, (c: CategoryHeader) => {
        if (c.id === categoryId) {
          c.subcategories = [];
          c.isActive = false;
        }
      });

      //If has a parent, make parent selected   
      var parentCat = newState.categories
        .find(x => x.subcategories != undefined &&
          x.subcategories.find(e => e.id === categoryId) != undefined);

      if (parentCat) {
        newState.filter.categoryId = parentCat.id;
        parentCat.isActive = true;
      }
      //if doesnt have a parent cat. delete selection 
      else {
        newState.filter.categoryId = null;
      }
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

      var newState = {
        ...state,
        products: result.products,
        brands: result.brands,
        maxPrice: result.maxPriceOption
      };
      // If there is no selected category, 
      //load the options given by thetext based search 
      if (newState.filter.categoryId == null) {
        newState.categories = result.categories;
      }  

      return newState;
    }
    default: {
      return state;
    }
  }
}