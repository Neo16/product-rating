import { Component, OnInit } from '@angular/core';
import { ProductCellData } from 'src/app/models/ProductCellData';
import { ProductService } from '../../services/product.service';
import { Store } from '@ngrx/store';
import { SearchState } from 'src/app/store/search-store/search.state';
import { SearchParams } from 'src/app/models/SearchParams';
import { store } from '@angular/core/src/render3';
import { Observable } from 'rxjs';
import { selectSearchState } from 'src/app/store/root-state';
import { FireSearchAction } from 'src/app/store/search-store/search.actions';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {

  productCells: ProductCellData[] = []; 
  
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
      console.log(searchState.filter);
    }); 
  } 
}
