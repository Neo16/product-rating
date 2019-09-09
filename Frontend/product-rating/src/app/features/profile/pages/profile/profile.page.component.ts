import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../../services/profile-service';
import { ProfileData } from 'src/app/models/profile/ProfileData';
import { EditProfileData } from 'src/app/models/profile/EditProdileData';
import { EditProfileFormComponent } from '../../components/edit-profile-form/edit-profile-form.component';


@Component({
  selector: 'app-profile',
  templateUrl: './profile.page.component.html',
  styleUrls: ['./profile.page.component.scss']
})
export class ProfilePageComponent implements OnInit {
  profile: ProfileData = null;
  editModel: EditProfileData = null;
  isEditing: boolean = false;

  constructor(private profileService: ProfileService) { }

  ngOnInit() {
    this.loadProfile();    
  } 

  loadProfile(){
    this.profileService.getMyProfile()
    .subscribe(result => {
      this.profile = result;
      this.editModel = new EditProfileData();
      this.editModel.email = this.profile.email;
      this.editModel.introduction = this.profile.introduction;    
      this.editModel.nationality = this.profile.nationality;
      this.editModel.nickName = this.profile.nickName;   
      this.editModel.pictureId = null;  
    })
  }

  saveProfile(){
    this.isEditing = false;
    console.log(this.editModel);
    this.profileService.editProfile(this.editModel)
     .subscribe(e => {
        this.loadProfile();
     });   
  }  
}
