import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { SearchState } from 'src/app/store/search-store/search.state';
import { Store } from '@ngrx/store';
import { selectSearchState } from 'src/app/store/root-state';
import { trigger, style, animate, transition, state } from '@angular/animations';
import { ProductCellData } from 'src/app/models/products/ProductCellData';
import { ChangePaginationAction, FireSearchAction } from 'src/app/store/search-store/search.actions';
import { PaginationParams } from 'src/app/models/search/PaginationParams';

@Component({
  selector: 'app-main.page',
  templateUrl: './main.page.component.html',
  styleUrls: ['./main.page.component.scss'],
  // animations: [
  //   trigger('fadeInOut', [
  //     state('void', style({
  //       display: 'none',
  //       opacity: 0
  //     })),
  //     transition('void => *', animate(300)),
  //     transition('* => void', animate(0))
  //   ]),     
  // ]
})
export class MainPageComponent implements OnInit {

  //sate
  getSearchState: Observable<SearchState>; 
  showPropertySearch: boolean = false;
  productList: ProductCellData[] = []; 
  pageSize: number = 21; 
  totalNumOfResults: number = 0;

   constructor(private store: Store<SearchState>) {
      this.getSearchState = this.store.select(selectSearchState);     
   }
  
  ngOnInit() {
    this.getSearchState.subscribe((searchState) => {
      this.showPropertySearch = searchState.filter.categoryId != null ||
         (searchState.products && searchState.products.length > 0);     

         this.productList = searchState.products;     
         this.pageSize = searchState.pagination.length;  
         this.totalNumOfResults = searchState.pagination.totalNumOfResults; 
    });   
    this.getSearchState.subscribe((searchState) => {           
    }); 
  }

  pageChange(page: number){
    if(page){     
      this.store.dispatch(new ChangePaginationAction({
        start: (page-1) * this.pageSize,
        length: this.pageSize 
      } as PaginationParams));    
      this.store.dispatch(new FireSearchAction());    
    }
  }

}
