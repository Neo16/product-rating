import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../../services/profile-service';
import { ModalService } from 'src/app/shared/services/modal-service';

@Component({
  selector: 'app-change-password-form',
  templateUrl: './change-password-form.component.html',
  styleUrls: ['./change-password-form.component.scss']
})
export class ChangePasswordFormComponent implements OnInit {

  currentPassword: string;
  newPassword: string;
  newPasswordAgain: string;

  constructor(private profileService: ProfileService,
    private modalService: ModalService) { }

  ngOnInit() {
  }

  changePassword() {
    if (this.newPassword != this.newPasswordAgain) {
      this.modalService.openInformationModal("Passwords don't match", "Please write the same password twice.");
    }
    else {
      this.profileService.changePassword({
        password: this.currentPassword,
        newPassword: this.newPassword
      }).subscribe(e => {
        this.modalService.openInformationModal("Success", "You successfully change your password.");
      }, error => this.modalService.openInformationModal(
        "Error",
        error.error[0].description || "Something went wrong.")
      )
    }
  }
}
