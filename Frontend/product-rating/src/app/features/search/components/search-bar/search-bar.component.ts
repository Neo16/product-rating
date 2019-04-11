import { Component, OnInit } from '@angular/core';
import { ProductOrder, ProductOrderDisplay } from 'src/app/models/ProductOrder';
import { Order, OrderDisplay } from 'src/app/models/Order';
import { SearchParams } from 'src/app/models/SearchParams';
import { Store } from '@ngrx/store';
import { SearchState } from 'src/app/store/search-store/search.state';
import { selectSearchState } from 'src/app/store/root-state';
import { Observable } from 'rxjs';
import { ChangeFilterAction } from 'src/app/store/search-store/search.actions';

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
  } 

  changeFilter(){       
    this.store.dispatch(new ChangeFilterAction(this.filterModel));
  }
}
