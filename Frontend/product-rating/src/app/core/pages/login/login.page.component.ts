import { Component, OnInit } from '@angular/core';
import { LogInAction } from '../../../store/account-store/account.actions';
import { Store } from '@ngrx/store';
import { selectAccountState } from '../../../store/root-state';
import { Observable } from 'rxjs';
import { AccountState } from '../../../store/account-store/account.state';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.component.html',
  styleUrls: ['./login.page.component.scss']
})
export class LoginPageComponent implements OnInit {
  username: string;
  password: string;
  getState: Observable<AccountState>;
  returnUrl: string;

  constructor(private acountStore: Store<AccountState>,
    private route: ActivatedRoute) { 
    this.getState = this.acountStore.select(selectAccountState);
  }

  ngOnInit() {
    this.returnUrl = this.route.snapshot.queryParams['return'] || '/';
  }

  login(): void {         
    const payload = {
      username: this.username,
      password: this.password,
      returnUrl: this.returnUrl
    };    
    this.acountStore.dispatch(new LogInAction(payload));
  }
}
