import { Component, OnInit, Input, Output } from '@angular/core';
import { EditProfileData } from 'src/app/models/profile/EditProdileData';
import { EventEmitter } from '@angular/core';

@Component({
  selector: 'app-edit-profile-form',
  templateUrl: './edit-profile-form.component.html',
  styleUrls: ['./edit-profile-form.component.scss']
})
export class EditProfileFormComponent implements OnInit {

  constructor() { }

  @Input()
  profile: EditProfileData; 
  @Output()  
  save = new EventEmitter();

  ngOnInit() {
  }

  submit(){
    this.save.emit(null);
  }
}
