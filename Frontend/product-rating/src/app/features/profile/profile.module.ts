import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileRoutingModule } from './profile-routing.module';
import { ProfilePageComponent } from './pages/profile/profile.page.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { ProfileService } from './services/profile-service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from 'src/app/core/services/token.service';
import { EditProfileFormComponent } from './components/edit-profile-form/edit-profile-form.component';
import { ProfileDataComponent } from './components/profile-data/profile-data.component';
import { ChangePasswordFormComponent } from './components/change-password-form/change-password-form.component';


@NgModule({
  declarations: [
    ProfilePageComponent,
    EditProfileFormComponent,
    ProfileDataComponent,
    ChangePasswordFormComponent
  ],
  providers: [
    ProfileService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }    
  ],
  imports: [
    CommonModule,
    ProfileRoutingModule,
    SharedModule   
  ]
})
export class ProfileModule { }
