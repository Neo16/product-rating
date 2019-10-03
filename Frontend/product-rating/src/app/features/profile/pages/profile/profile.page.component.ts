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

  getAccountState: Observable<AccountState>;

  constructor(private profileService: ProfileService,
    private pictureservice: PictureService,
    private acountStore: Store<AccountState>) {
    this.getAccountState = this.acountStore.select(selectAccountState);
  }

  ngOnInit() {
    this.getAccountState.subscribe((accountState) => {
      this.userId = accountState.user.id;
      console.log(this.userId);
    });   
     //TODO: ha van url.-be id, akkor így, ha nem, akkor bejeletkezettt user alapján 
     this.loadProfile();
  }

  loadProfile() {
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

  saveProfile() {
    this.isEditing = false;
    console.log(this.editModel);
    this.profileService.editProfile(this.editModel)
      .subscribe(e => {
        this.loadProfile();
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
