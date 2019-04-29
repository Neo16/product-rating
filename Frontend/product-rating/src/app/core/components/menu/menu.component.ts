import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { selectAccountState } from '../../../store/root-state';
import { Observable } from 'rxjs';
import { AccountState } from '../../../store/account-store/account.state';
import { LogOutAction, LogInSuccessAction } from 'src/app/store/account-store/account.actions';
import { LoginResultData } from 'src/app/models/LoginResultData';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {

  //component data
  isLoggedIn: boolean = false;
  userName: string = null;
  roles: string[] = [];

  //state
  getAccountState: Observable<AccountState>;

  constructor(private acccountStore: Store<AccountState>) {
    this.getAccountState = this.acccountStore.select(selectAccountState);
  }

  ngOnInit() {
    this.getAccountState.subscribe((accountState) => {
      this.isLoggedIn = accountState.isAuthenticated;
      if (this.isLoggedIn) {
        this.userName = accountState.user.username;
        this.roles = accountState.user.roles;
      }      
    });
    if (!this.isLoggedIn) {
      this.loginFromLocalStorage();
    }
  }

  loginFromLocalStorage() {
    var userName = localStorage.getItem('productrating-token');
    var userToken = localStorage.getItem('productrating-username');
    var userRoles = JSON.parse(localStorage.getItem('productrating-userroles')) as String[];

    if (userName && userToken) {
      this.acccountStore.dispatch(new LogInSuccessAction({
        userToken: userToken,
        userName: userName,
        userRoles: userRoles
      } as LoginResultData));
    }
  }

  logout() {
    this.acccountStore.dispatch(new LogOutAction());
  }
}
