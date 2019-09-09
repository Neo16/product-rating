import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { SearchState } from 'src/app/store/search-store/search.state';
import { Store } from '@ngrx/store';
import { selectSearchState } from 'src/app/store/root-state';
import { trigger, style, animate, transition, state } from '@angular/animations';

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

   constructor(private store: Store<SearchState>) {
      this.getSearchState = this.store.select(selectSearchState);     
   }
  
  ngOnInit() {
    this.getSearchState.subscribe((searchState) => {
      this.showPropertySearch = searchState.filter.categoryId != null ||
         (searchState.products && searchState.products.length > 0);      
    });   
  }

}
