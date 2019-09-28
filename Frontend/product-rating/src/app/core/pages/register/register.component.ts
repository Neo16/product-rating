import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { RegisterData } from 'src/app/models/RegisterData';
import { RoleDisplay, Role } from 'src/app/models/Role';
import { Router } from '@angular/router';
import { ModalService } from 'src/app/shared/services/modal-service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  user: RegisterData = new RegisterData();
  roleDisplay = RoleDisplay;
  role = Role;

  constructor(private accountService: AccountService,
    private router: Router,
    private modalService: ModalService) { }

  register() {
    this.accountService.register(this.user)
      .subscribe(e => {
        this.modalService.openInformationModal("Welcome to ProductRating!", "Click here to login.")
          .then(yep => {
            this.router.navigate(['/login']);
          })
      }, e => {  
        this.modalService.openInformationModal("Failed", e.error);
      });
  }
}


