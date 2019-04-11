import { Injectable } from '@angular/core';
import { Effect, ofType, Actions } from '@ngrx/effects';
import { tap, map, switchMap, withLatestFrom} from 'rxjs/operators';
import {  FireSearchAction, SearchActionTypes, SearchSuccessAction, SearchFailureAction } from './search.actions';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/of';
import { Router } from '@angular/router';
import { ProductService } from 'src/app/features/products/services/product.service';
import { SearchResult } from 'src/app/models/SearchResult';
import { SearchState } from './search.state';
import { Store } from '@ngrx/store';
import { selectSearchState } from '../root-state';

@Injectable()
export class SearchEffects {

    constructor(
        private store: Store<SearchState>,
        private actions: Actions,
        private productService : ProductService,    
        public router: Router 
      ) {}

    @Effect()
    public FireSearch: Observable<any> = this.actions.pipe(
      ofType(SearchActionTypes.FIRE_SERACH),
      withLatestFrom(this.store.select(selectSearchState)),
      map((actionAndState) => actionAndState[1].filter),
      switchMap(payload => {
        return this.productService.searchProducts(payload)
          .map((result: SearchResult) => {  
            console.log('asd');         
            return new SearchSuccessAction(result);
          })
          .catch((error) => { 
            console.log(error);
            return Observable.of(new SearchFailureAction({ error: error }));
          });
      }));    
}