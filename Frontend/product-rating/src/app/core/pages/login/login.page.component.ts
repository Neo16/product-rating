import { Component, OnInit } from '@angular/core';
import { LogInAction } from '../../../store/account-store/account.actions';
import { Store } from '@ngrx/store';
import { AppState, selectAccountState } from '../../../store/root-state';
import { Observable } from 'rxjs';
import { AccountState } from '../../../store/account-store/account.state';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.component.html',
  styleUrls: ['./login.page.component.scss']
})
export class LoginPageComponent implements OnInit {

  username: string = "user@productrating.com";
  password: string = "Asdf123!";
  getState: Observable<AccountState>;
  
  constructor(private store: Store<AppState>) { 
    this.getState = this.store.select(selectAccountState);
  }

  ngOnInit() {
    this.getState.subscribe((state) => {
       console.log(JSON.stringify(state));
    });
  }

  login(): void {   
    const payload = {
      username: this.username,
      password: this.password
    };    
    this.store.dispatch(new LogInAction(payload));
  }
}
