import { initialState, AccountState } from './account.state';
import { AccountActions, AccountActionTypes } from './account.actions';


export function accountReducer(state = initialState, action: AccountActions): AccountState {
    switch (action.type) {
      case AccountActionTypes.LOGIN_SUCCESS: {
        return {
          ...state,
          isAuthenticated: true,
          user: {
            token: action.payload.token,
            username: action.payload.username
          },
          errorMessage: null
        };
      }
      default: {
        return state;
      }
    }
  }