import { AccountState } from './account-store/account.state';
import { accuntReducer } from './account-store/account.reducer';
import { createFeatureSelector } from '@ngrx/store';

export interface AppState {
    accountState: AccountState;
}

export const reducers = {
    account: accuntReducer
};

export const selectAccountState = createFeatureSelector<AccountState>('account');