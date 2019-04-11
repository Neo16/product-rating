import { Component, OnInit } from '@angular/core';
import { SearchState } from 'src/app/store/search-store/search.state';
import { Store } from '@ngrx/store';
import { selectSearchState } from 'src/app/store/root-state';
import { Observable } from 'rxjs';
import { CategoryHeader } from 'src/app/models/CategoryHeader';
import { BrandHeader } from 'src/app/models/BrandHeader';

@Component({
  selector: 'app-property-search',
  templateUrl: './property-search.component.html',
  styleUrls: ['./property-search.component.scss']
})
export class PropertySearchComponent implements OnInit {
  
  //sate
  getSearchState: Observable<SearchState>;
  categories: CategoryHeader[] = [];
  brands: BrandHeader[] = [];

  constructor(private store: Store<SearchState>) {
    this.getSearchState = this.store.select(selectSearchState);     
  }

  ngOnInit() {
    this.getSearchState.subscribe((searchState) => {
       this.categories = searchState.categories;
    }); 
  }
}
