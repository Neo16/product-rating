import { Component, OnInit } from '@angular/core';
import { ProductCellData } from 'src/app/models/products/ProductCellData';
import { Store } from '@ngrx/store';
import { SearchState } from 'src/app/store/search-store/search.state';
import { Observable } from 'rxjs';
import { selectSearchState } from 'src/app/store/root-state';
import { ChangePaginationAction, FireSearchAction } from 'src/app/store/search-store/search.actions';
import { PaginationParams } from 'src/app/models/search/PaginationParams';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {

  productCells: ProductCellData[] = []; 
  pageNum: number;
  pageSize: number = 21; 
  totalNumOfResults: number = 0;

  //sate
  getSearchState: Observable<SearchState>;

  constructor(
    private store: Store<SearchState>
  ){
      this.getSearchState = this.store.select(selectSearchState);
  }

  ngOnInit() {      
    this.getSearchState.subscribe((searchState) => {     
      this.productCells = searchState.products;     
      this.pageSize = searchState.pagination.length;  
      this.totalNumOfResults = searchState.pagination.totalNumOfResults;
    }); 
  } 

  pageChange(page){
    if(page){     
      this.store.dispatch(new ChangePaginationAction({
        start: (page-1) * this.pageSize,
        length: this.pageSize 
      } as PaginationParams));    
      this.store.dispatch(new FireSearchAction());    
    }
  }
}
