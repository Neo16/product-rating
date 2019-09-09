import { Component, OnInit, Input } from '@angular/core';
import { ProfileData } from 'src/app/models/profile/ProfileData';
import { PictureService } from 'src/app/features/manage-products/services/picture-service';

@Component({
  selector: 'app-profile-data',
  templateUrl: './profile-data.component.html',
  styleUrls: ['./profile-data.component.scss']
})
export class ProfileDataComponent implements OnInit {

  constructor() { }

  @Input()
  profile: ProfileData;

  ngOnInit() {
  }  
}
