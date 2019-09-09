import { AccountState } from './account-store/account.state';
import { accountReducer } from './account-store/account.reducer';
import { createFeatureSelector } from '@ngrx/store';
import { SearchState } from './search-store/search.state';
import { searchReducer } from './search-store/search.reducer';

export interface AppState {
    accountState: AccountState;
    searchState: SearchState
}

export const reducers = {
    account: accountReducer,
    search: searchReducer
};

export const selectAccountState = createFeatureSelector<AccountState>('account');
export const selectSearchState = createFeatureSelector<SearchState>('search');