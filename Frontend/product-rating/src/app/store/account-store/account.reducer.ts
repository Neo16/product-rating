import { initialState, AccountState } from './account.state';
import { AccountActions, AccountActionTypes } from './account.actions';
import { LoginResultData } from 'src/app/models/LoginResultData';


export function accountReducer(state: AccountState = initialState, action: AccountActions): AccountState {
    switch (action.type) {
      case AccountActionTypes.LOGIN_SUCCESS: {
        console.log("login success action called");
        var loginResponse = action.payload as LoginResultData;       
        
        var newState: AccountState = {
          isAuthenticated: true,
          user: {
            token: loginResponse.userToken,
            username: loginResponse.userName,
            roles: loginResponse.userRoles
          },
          errorMessage: null
        };           
        return newState;
       }       
      default: {       
        return state;
      }
    }
  }