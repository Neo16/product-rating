import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../../services/profile-service';
import { ProfileData } from 'src/app/models/profile/ProfileData';
import { EditProfileData } from 'src/app/models/profile/EditProdileData';
import { EditProfileFormComponent } from '../../components/edit-profile-form/edit-profile-form.component';
import { PictureService } from 'src/app/features/manage-products/services/picture-service';
import { AccountState } from 'src/app/store/account-store/account.state';
import { Store } from '@ngrx/store';
import { selectAccountState } from 'src/app/store/root-state';
import { Observable } from 'rxjs';
import { ActivatedRoute, ParamMap } from '@angular/router';


@Component({
  selector: 'app-profile',
  templateUrl: './profile.page.component.html',
  styleUrls: ['./profile.page.component.scss']
})
export class ProfilePageComponent implements OnInit {
  userId: string;
  profile: ProfileData = null; 
  editModel: EditProfileData = null;
  isEditing: boolean = false;
  profileId: string;
  isOwner: boolean = false;

  getAccountState: Observable<AccountState>;  

  constructor(
    private route: ActivatedRoute,
    private profileService: ProfileService,
    private pictureservice: PictureService,
    private acountStore: Store<AccountState>) {
    this.getAccountState = this.acountStore.select(selectAccountState);
  }

  ngOnInit() {
    this.route.paramMap.subscribe((params: ParamMap) => {
      this.userId = params.get('id');
      console.log(this.profileId);

      // View other user's profile 
      if (this.userId) {
         this.loadOtherProfile();
      }
       // View own 
      else {
        this.getAccountState.subscribe((accountState) => {
          this.userId = accountState.user.id;         
        });
        this.loadMyProfile();
      }

      var userRoles = JSON.parse(localStorage.getItem('productrating-userroles')) as string[];
      this.isOwner = userRoles.some(e => e == "SHOP_OWNER")
    });      
  }

  loadMyProfile(){   
    this.profileService.getMyProfile()
    .subscribe(result => {
      this.profile = result;
      this.editModel = new EditProfileData();
      this.editModel.email = this.profile.email;
      this.editModel.introduction = this.profile.introduction;
      this.editModel.nationality = this.profile.nationality;
      this.editModel.nickName = this.profile.nickName;
      this.editModel.pictureId = null;
    });
  }

  loadOtherProfile(){   
    this.profileService.getProfileById(this.userId)
    .subscribe(result => {
      this.profile = result;   
    });
  }

  saveProfile() {
    this.isEditing = false;
    console.log(this.editModel);
    this.profileService.editProfile(this.editModel)
      .subscribe(e => {
        this.loadMyProfile();
      });
  }

  processAvatar(imageInput: any) {
    const file: File = imageInput.files[0];
    const reader = new FileReader();

    reader.addEventListener('load', (event: any) => {
      this.pictureservice.uploadImage(file).subscribe(
        (res) => {         
          this.profile.avatar = res.data;
          this.editModel.pictureId = res.id;
        },
        (err) => {
          alert("Not OK");
          console.log(err);
        })
    });
    reader.readAsDataURL(file);
  }
}
