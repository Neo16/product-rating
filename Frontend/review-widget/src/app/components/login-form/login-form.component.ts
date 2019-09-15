import { Component, OnInit, EventEmitter } from '@angular/core';
import { LoginService } from 'src/app/services/login-service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent implements OnInit {

  username: string = "user@productrating.com";
  password: string = "Asdf123!";

  constructor(
    private loginService: LoginService,
    public activeModal: NgbActiveModal) { }

  ngOnInit() {
  }

  login(){
    this.loginService.logIn(this.username, this.password)
    .subscribe(e => {
        if (e.userToken != null){
          localStorage.setItem('productrating-token', e.userToken);     
          this.activeModal.close("Submit");
        }
        else{
          //TODO: login failure-t kezelni 
          this.activeModal.close("Submit");
        }    
    });

   
  }

}
