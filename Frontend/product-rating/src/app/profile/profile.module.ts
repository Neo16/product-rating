import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileRoutingModule } from './profile-routing.module';
import { MyProfilePageComponent } from './pages/my-profile/my-profile.page.component';

@NgModule({
  declarations: [
    MyProfilePageComponent
  ],
  imports: [
    CommonModule,
    ProfileRoutingModule
  ]
})
export class ProfileModule { }
