import { Action } from '@ngrx/store';
import { LoginResultData } from 'src/app/models/LoginResultData';

export enum AccountActionTypes {
    LOGIN = '[Auth] Login',
    LOGIN_SUCCESS = '[Auth] Login Success',
    LOGIN_FAILURE = '[Auth] Login Failure',   
    LOGOUT = '[Auth] Logout',
  }

export class LogInAction implements Action {
    readonly type = AccountActionTypes.LOGIN;
    constructor(public payload: any) {}
}
  
export class LogInSuccessAction implements Action {
    readonly type = AccountActionTypes.LOGIN_SUCCESS;
    constructor(public payload: LoginResultData) {}
}
  
export class LogInFailureAction implements Action {
    readonly type = AccountActionTypes.LOGIN_FAILURE;
    constructor(public payload: any) {}
}

export class LogOutAction implements Action {
    readonly type = AccountActionTypes.LOGOUT;
  }
  

export type AccountActions = LogInAction | LogInSuccessAction | LogInFailureAction | LogOutAction;
    