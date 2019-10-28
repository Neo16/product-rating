import { Injectable } from '@angular/core';
import { Effect, ofType, Actions } from '@ngrx/effects';
import { tap, map, switchMap, withLatestFrom} from 'rxjs/operators';
import { LogInSuccessAction, LogInFailureAction, LogInAction, AccountActionTypes } from './account.actions';
import { AccountService } from '../../core/services/account.service';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/of';
import { Router } from '@angular/router';
import { LoginResultData } from 'src/app/models/LoginResultData';
import { User } from 'src/app/models/User';
import { Store } from '@ngrx/store';
import { AccountState } from './account.state';
import { selectAccountState } from '../root-state';


@Injectable()
export class AcccountEffects {

    constructor(
        private actions: Actions,
        private store: Store<AccountState>,
        private accountService: AccountService,    
        public router: Router 
      ) {}

    @Effect()
    public LogIn: Observable<any> = this.actions.pipe(
      ofType(AccountActionTypes.LOGIN),
      map((action: LogInAction) => action.payload),     
      switchMap(payload => {     
        return this.accountService.logIn(payload.username, payload.password)
          .map((loginResult: LoginResultData) => {                                   
            return new LogInSuccessAction(loginResult);
          })
          .catch((error) => {           
            return Observable.of(new LogInFailureAction({ error: error }));
          });
      }));

      @Effect({ dispatch: false })
      LogInSuccess: Observable<any> = this.actions.pipe(
        ofType(AccountActionTypes.LOGIN_SUCCESS),
        withLatestFrom(this.store.select(selectAccountState)),         
        tap((result : [LogInSuccessAction, AccountState]) => {               
          localStorage.setItem('productrating-token', result[0].payload.userToken);   
          localStorage.setItem('productrating-username', result[0].payload.userName); 
          localStorage.setItem('productrating-userroles', JSON.stringify(result[0].payload.userRoles));  
          localStorage.setItem('productrating-userid', JSON.stringify(result[0].payload.userId));                  
          this.router.navigateByUrl(result[1].loginReturnUrl);
        })
      );
      

      @Effect({ dispatch: false })
      public LogOut: Observable<any> = this.actions.pipe(
        ofType(AccountActionTypes.LOGOUT),
        tap((user) => {
          localStorage.removeItem('productrating-token');   
          localStorage.removeItem('productrating-username'); 
          localStorage.removeItem('productrating-userroles');         
        })
      );      
}