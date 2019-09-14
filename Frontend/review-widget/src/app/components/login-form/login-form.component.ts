import { Component, OnInit, EventEmitter } from '@angular/core';
import { LoginService } from 'src/app/services/login-service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent implements OnInit {

  username: string = "user@productrating.com";
  password: string = "Asdf123!";

  loginSuccessEvent: EventEmitter<any> = new EventEmitter<any>();

  constructor(private loginService: LoginService) { }

  ngOnInit() {
  }

  login(){
    this.loginService.logIn(this.username, this.password)
    .subscribe(e => {
        if (e.userToken != null){
          this.loginSuccessEvent.emit();
        }
        //TODO: login failure-t kezelni 
    });
  }

}
