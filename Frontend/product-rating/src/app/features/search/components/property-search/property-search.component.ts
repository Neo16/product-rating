import { Component, OnInit } from '@angular/core';
import { SearchState } from 'src/app/store/search-store/search.state';
import { Store } from '@ngrx/store';
import { selectSearchState } from 'src/app/store/root-state';
import { Observable } from 'rxjs';
import { CategoryHeader } from 'src/app/models/CategoryHeader';
import { BrandHeader } from 'src/app/models/BrandHeader';
import { ChangeFilterAction, FireSearchAction, AddCategoryFilterAction, RemoveCategoryFilterAction } from 'src/app/store/search-store/search.actions';
import { SearchParams } from 'src/app/models/SearchParams';

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

  constructor(private store: Store<SearchState>) {
    this.getSearchState = this.store.select(selectSearchState);     
  }

  ngOnInit() {
    this.getSearchState.subscribe((searchState) => {
       this.categories = searchState.categories;
       this.filter = searchState.filter;
    }); 
  }

  selectCategory(categoryId: string){   
    this.store.dispatch(new AddCategoryFilterAction(categoryId));
    this.store.dispatch(new FireSearchAction());

    //Todo, call categories/id/subcategories action and set this.subcategories... 
  }

  unSelectCategory(){ 
    this.store.dispatch(new RemoveCategoryFilterAction());   
    this.store.dispatch(new FireSearchAction());

    //todo clear subcategories
  }
}
