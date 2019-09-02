import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileRoutingModule } from './profile-routing.module';
import { ProfilePageComponent } from './pages/profile/profile.page.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { ProfileService } from './services/profile-service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from 'src/app/core/services/token.service';

@NgModule({
  declarations: [
    ProfilePageComponent
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
