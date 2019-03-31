import { Injectable } from '@angular/core';
import { Effect, ofType, Actions } from '@ngrx/effects';
import { tap, map, switchMap} from 'rxjs/operators';
import { AccountActions, LogInSuccessAction, LogInFailureAction, LogInAction, AccountActionTypes } from './account.actions';
import { AccountService } from '../../core/services/account.service';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/of';
import { Router } from '@angular/router';


@Injectable()
export class AcccountEffects {

    constructor(
        private actions: Actions,
        private accountService: AccountService,    
        public router: Router 
      ) {}

    @Effect()
    public LogIn: Observable<any> = this.actions.pipe(
      ofType(AccountActionTypes.LOGIN),
      map((action: LogInAction) => action.payload),
      switchMap(payload => {
        return this.accountService.logIn(payload.username, payload.password)
          .map((user) => {           
            return new LogInSuccessAction({token: user.userToken, email: payload.userName});
          })
          .catch((error) => {
            console.log(error);
            return Observable.of(new LogInFailureAction({ error: error }));
          });
      }));

      @Effect({ dispatch: false })
      LogInSuccess: Observable<any> = this.actions.pipe(
        ofType(AccountActionTypes.LOGIN_SUCCESS),
        tap((user) => {
          localStorage.setItem('token', user.payload.token);
          this.router.navigateByUrl('/profile');
        })
      );

      @Effect({ dispatch: false })
      public LogOut: Observable<any> = this.actions.pipe(
        ofType(AccountActionTypes.LOGOUT),
        tap((user) => {
          localStorage.removeItem('token');
        })
      );      
}