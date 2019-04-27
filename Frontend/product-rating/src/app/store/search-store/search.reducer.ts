import { initialState, SearchState } from './search.state';
import {
  SearchActionTypes, ChangeFilterAction, SearchSuccessAction,
  AddCategoryFilterAction, RemoveCategoryFilterAction, AddBrandFilterAction, RemoveBrandFilterAction, ChangeOrderAction, ChangeProductOrderAction, ChangePaginationAction
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
    case SearchActionTypes.ADD_BRAND_FILTER: {
      var brandId = (action as AddBrandFilterAction).payload;
      var newState = {
        ...state,
        filter: {
          ...state.filter,
          brandIds: state.filter.brandIds.concat(brandId)
        }
      }    
      return newState;
    }
    case SearchActionTypes.REMOVE_BRAND_FILTER: {
      var brandId = (action as RemoveBrandFilterAction).payload;
      var newState = {
        ...state,
        filter: {
          ...state.filter,
          brandIds: state.filter.brandIds.filter(e => e != brandId)
        }
      }     
      return newState;
    }
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
    case SearchActionTypes.CHANGE_PAGINATION: {
      var newParams = (action as ChangePaginationAction).payload;
      console.log(newParams);
      return {
        ...state,
        pagination: Object.assign(state.pagination, newParams)
      };  
    }
    case SearchActionTypes.CHANGE_PRODUCT_ORDER: {
      var newProductOrder: number = (action as ChangeProductOrderAction).payload;
      var newState = {
        ...state,
      };
      newState.filter.orderBy = newProductOrder;
      return newState;
    }
    case SearchActionTypes.CHANGE_ORDER: {
      var newOrder: number = (action as ChangeOrderAction).payload;
      var newState = {
        ...state,
      };
      newState.filter.order = newOrder;
      return newState;
    }    
    case SearchActionTypes.SEARCH_SUCCESS: {
      var result = (action as SearchSuccessAction).payload;

      var newState = {
        ...state,
        products: result.products,
        brands: result.brands,
        maxPrice: result.maxPriceOption,
        pagination: {
          ... state.pagination,
          totalNumOfResults: result.totalNumOfResults
        }
      } as SearchState;     

      // Only reload categories if there is no selected category 
      if (newState.filter.categoryId == null) {
        newState.categories = result.categories;
      }          

      // Check all brands on ui 
      newState.brands.forEach(f => f.isChecked = true);   
      // Make selected brand unchecked 
      newState.brands.filter(e => newState.filter.brandIds.indexOf(e.id) != -1)
         .forEach(f =>f.isChecked = false);
         
      return newState;
    }
    default: {
      return state;
    }
  }
}