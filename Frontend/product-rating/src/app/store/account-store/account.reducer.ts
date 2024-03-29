import { initialState, AccountState } from './account.state';
import { AccountActions, AccountActionTypes, LogInSuccessAction } from './account.actions';
import { LoginResultData } from 'src/app/models/LoginResultData';


export function accountReducer(state: AccountState = initialState, action: AccountActions): AccountState {
  switch (action.type) {
    case AccountActionTypes.LOGOUT: {
      return initialState;
    }
    case AccountActionTypes.LOGIN: {
      return {
        ...state,
        loginReturnUrl: action.payload.returnUrl
      };
    }
    case AccountActionTypes.LOGIN_FAILURE: {
      var errorResponse = action.payload;
      if (errorResponse.error.error) {
        return {
          ...state,
          errorMessage: errorResponse.error.error
        };
      }
      else {
        return {
          ...state,
          errorMessage: "Login failed."
        };
      }
    }
    case AccountActionTypes.LOGIN_SUCCESS: {
      var loginResponse = action.payload as LoginResultData;

      var newState: AccountState = {
        isAuthenticated: true,
        user: {
          token: loginResponse.userToken,
          username: loginResponse.userName,
          roles: loginResponse.userRoles,
          id: loginResponse.userId
        },
        errorMessage: null,
        loginReturnUrl: state.loginReturnUrl
      };
      return newState;
    }
    case AccountActionTypes.LOGIN_LOADED: {
      var loginResponse = action.payload as LoginResultData;

      var newState: AccountState = {
        isAuthenticated: true,
        user: {
          token: loginResponse.userToken,
          username: loginResponse.userName,
          roles: loginResponse.userRoles,
          id: loginResponse.userId
        },
        errorMessage: null,
        loginReturnUrl: null
      };
      return newState;
    }
    default: {
      return state;
    }
  }
}