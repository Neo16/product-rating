import { Component, OnInit } from '@angular/core';
import { ProductOrder, ProductOrderDisplay } from 'src/app/models/search/ProductOrder';
import { Order, OrderDisplay } from 'src/app/models/search/Order';
import { SearchParams } from 'src/app/models/search/SearchParams';
import { Store } from '@ngrx/store';
import { SearchState } from 'src/app/store/search-store/search.state';
import { selectSearchState } from 'src/app/store/root-state';
import { Observable } from 'rxjs';
import { ChangeFilterAction, FireSearchAction, ChangeProductOrderAction, ChangeOrderAction } from 'src/app/store/search-store/search.actions';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.scss']
})
export class SearchBarComponent implements OnInit {
  // Options 
  productOrder = ProductOrder;
  productOrderDisplay = ProductOrderDisplay;
  order = Order;
  orderDisplay = OrderDisplay;

  //Filter 
  filterModel = new SearchParams();

  //sate
  getSearchState: Observable<SearchState>; 

  constructor(private store: Store<SearchState>) {
      this.getSearchState = this.store.select(selectSearchState);     
  }

  ngOnInit() {
    this.store.dispatch(new FireSearchAction());   
    this.getSearchState.subscribe((searchState) => {
      this.filterModel = searchState.filter;       
    });       
  } 

  changeFilter(){       
    this.store.dispatch(new ChangeFilterAction(this.filterModel));    
    this.store.dispatch(new FireSearchAction());    
  }

  onProductOrderChange(newOrder){
    this.store.dispatch(new ChangeProductOrderAction(newOrder));    
    this.store.dispatch(new FireSearchAction());    
  }

  onOrderChange(newOrder){
    this.store.dispatch(new ChangeOrderAction(newOrder));    
    this.store.dispatch(new FireSearchAction());    
  }
}
