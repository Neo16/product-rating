import { Component, OnInit } from '@angular/core';
import { SearchState } from 'src/app/store/search-store/search.state';
import { Store } from '@ngrx/store';
import { selectSearchState } from 'src/app/store/root-state';
import { Observable } from 'rxjs';
import { CategoryHeader } from 'src/app/models/search/CategoryHeader';
import { BrandHeader } from 'src/app/models/search/BrandHeader';
import { ChangeFilterAction, FireSearchAction, AddCategoryFilterAction, RemoveCategoryFilterAction, AddBrandFilterAction, RemoveBrandFilterAction } from 'src/app/store/search-store/search.actions';
import { SearchParams } from 'src/app/models/search/SearchParams';
import { SearchHelperService } from '../../services/search-helper.service';
import { Options, ChangeContext } from 'ng5-slider';

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
  filter = new SearchParams();

  //price range slider
  sliderOptions: Options = {
    floor: 0,
    ceil: 10000000,
    step: 5,
    minRange: 10,    
  };

  constructor(
    private store: Store<SearchState>,
    private helperService: SearchHelperService) {
       this.getSearchState = this.store.select(selectSearchState);
  }

  changeSliderMax(max: number){
    const newOptions: Options = Object.assign({}, this.sliderOptions);
    newOptions.ceil = max;    
    this.sliderOptions = newOptions;
  }

  onUserPriceSliderChange(changeContext: ChangeContext): void {
     this.store.dispatch(new ChangeFilterAction(this.filter));
     this.store.dispatch(new FireSearchAction());
  }

  ngOnInit() {
    this.getSearchState.subscribe((searchState) => {
      this.filter = searchState.filter;  
      this.brands = searchState.brands;
      this.categories = searchState.categories;   
      this.changeSliderMax(searchState.maxPrice);          
    });
  }

  selectCategory(categoryId: string) {    
    //load subcategories 
    //Todo: create effect from this 
    this.helperService.getSubCategoriesOf(categoryId)
      .subscribe(result => {
        var cat = this.categories
          .find(x => x.id === categoryId);
        if (cat) {
          cat.subcategories = result;
        }
      });
    this.store.dispatch(new AddCategoryFilterAction(categoryId));
    this.store.dispatch(new FireSearchAction());
  }

  unSelectCategory(categoryId: string) {  
      this.store.dispatch(new RemoveCategoryFilterAction(categoryId));
      this.store.dispatch(new FireSearchAction());
  } 

  selectBrand(brandId, event) {   
    if (event.target.checked){     
      this.store.dispatch(new RemoveBrandFilterAction(brandId));
    }
    else {
      this.store.dispatch(new AddBrandFilterAction(brandId));   
    }  
    this.store.dispatch(new FireSearchAction());
  }
}
