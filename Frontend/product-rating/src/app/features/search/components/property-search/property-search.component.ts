import { Component, OnInit } from '@angular/core';
import { SearchState } from 'src/app/store/search-store/search.state';
import { Store } from '@ngrx/store';
import { selectSearchState } from 'src/app/store/root-state';
import { Observable } from 'rxjs';
import { CategoryHeader } from 'src/app/models/CategoryHeader';
import { BrandHeader } from 'src/app/models/BrandHeader';
import { ChangeFilterAction, FireSearchAction, AddCategoryFilterAction, RemoveCategoryFilterAction } from 'src/app/store/search-store/search.actions';
import { SearchParams } from 'src/app/models/SearchParams';
import { SearchHelperService } from '../../services/search-helper.service';

@Component({
  selector: 'app-property-search',
  templateUrl: './property-search.component.html',
  styleUrls: ['./property-search.component.scss']
})
export class PropertySearchComponent implements OnInit {
  
  //sate
  getSearchState: Observable<SearchState>;
  categories: CategoryHeader[] = [];
  subcategories: CategoryHeader[] = [];
  brands: BrandHeader[] = [];
  filter = new SearchParams();  

  constructor(
    private store: Store<SearchState>,
    private helperService: SearchHelperService) {
    this.getSearchState = this.store.select(selectSearchState);     
  }

  ngOnInit() {
    this.getSearchState.subscribe((searchState) => {
      this.filter = searchState.filter;
      // If there is no selected category, 
      //load the options given by thetext based search 
      if (this.filter.categoryId == null){
        this.categories = searchState.categories;
      }    
    }); 
  }

  selectCategory(categoryId: string){     
    //create effect from this maybe  
    this.helperService.getSubCategoriesOf(categoryId)
      .subscribe(result => {       
         var cat = this.categories
            .find(x => x.id === categoryId);
            if (cat){
              cat.subcategories = result;
            }
      });
    this.store.dispatch(new AddCategoryFilterAction(categoryId));
    this.store.dispatch(new FireSearchAction());  
  }

  unSelectCategory(categoryId: string){   
   var cat = this.categories
      .find(x => x.id === categoryId);
   if (cat){
     cat.subcategories = [];
   }

    //Todo: change category filter to parent if any 
    this.store.dispatch(new RemoveCategoryFilterAction());   
    this.store.dispatch(new FireSearchAction()); 
  }
}
