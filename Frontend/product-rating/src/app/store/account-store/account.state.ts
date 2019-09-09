import { User } from '../../models/User';

export interface AccountState {
    // is a user authenticated?
    isAuthenticated: boolean;
    // if authenticated, there should be a user object
    user: User | null;
    // error message
    errorMessage: string | null;
    loginReturnUrl: string | null;
}

export const initialState: AccountState = {
    isAuthenticated: false,
    user: null,
    errorMessage: null,
    loginReturnUrl: null,
 };