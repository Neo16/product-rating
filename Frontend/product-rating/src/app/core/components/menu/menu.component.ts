import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { selectAccountState } from '../../../store/root-state';
import { Observable } from 'rxjs';
import { AccountState } from '../../../store/account-store/account.state';

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
       console.log(accountState);  
       this.isLoggedIn = accountState.isAuthenticated;
       if (this.isLoggedIn){
          this.userName = accountState.user.username;
          this.roles = accountState.user.roles;
       }      
   });
  }

}
