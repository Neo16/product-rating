import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../../services/profile-service';
import { ProfileData } from 'src/app/models/profile/ProfileData';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.page.component.html',
  styleUrls: ['./profile.page.component.scss']
})
export class ProfilePageComponent implements OnInit {

  profile: ProfileData = null;
  isEditing: boolean = false;
  isMine: boolean = false;

  constructor(private profileService: ProfileService) { }

  ngOnInit() {
     this.profileService.getMyProfile()
      .subscribe(result => {
        this.profile = result;
      })
  } 

}
