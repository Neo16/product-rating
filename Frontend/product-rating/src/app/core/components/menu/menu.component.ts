import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { selectAccountState } from '../../../store/root-state';
import { Observable } from 'rxjs';
import { AccountState } from '../../../store/account-store/account.state';
import { LogOutAction, LogInSuccessAction, LoadedLoginDataAction } from 'src/app/store/account-store/account.actions';
import { LoginResultData } from 'src/app/models/LoginResultData';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {

  //component data
  isNavbarCollapsed = true;
  isLoggedIn: boolean = false;
  userName: string = null;
  roles: string[] = [];

  isOwner: boolean = false;
  isAdmin: boolean = false;

  //state
  getAccountState: Observable<AccountState>;

  constructor(private acccountStore: Store<AccountState>) {
    this.getAccountState = this.acccountStore.select(selectAccountState);
  }

  ngOnInit() {
    this.getAccountState.subscribe((accountState) => {
      this.isLoggedIn = accountState.isAuthenticated;
      if (this.isLoggedIn) {
        console.log(accountState);
        this.userName = accountState.user.username;
        this.roles = accountState.user.roles;
        this.setRoleFlags(this.roles);
      }
    });
    if (!this.isLoggedIn) {
      this.loginFromLocalStorage();
    }
  }

  loginFromLocalStorage() {
    var userToken = localStorage.getItem('productrating-token');
    var userName = localStorage.getItem('productrating-username');
    var userRoles = JSON.parse(localStorage.getItem('productrating-userroles')) as string[];
    var userId = JSON.parse(localStorage.getItem('productrating-userid'));

    if (userName && userToken) {
      this.setRoleFlags(userRoles);
      this.acccountStore.dispatch(new LoadedLoginDataAction({
        userToken: userToken,
        userName: userName,
        userRoles: userRoles,
        userId: userId
      } as LoginResultData));
    }
  }

  setRoleFlags(roles: string[]) {
    this.isAdmin = roles.some(e => e == "ADMIN");
    this.isOwner = roles.some(e => e == "SHOP_OWNER")
  }

  logout() {
    this.acccountStore.dispatch(new LogOutAction());
  }
}
