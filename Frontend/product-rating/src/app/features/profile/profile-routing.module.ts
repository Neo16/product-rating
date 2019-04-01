import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MyProfilePageComponent } from './pages/my-profile/my-profile.page.component';
import { AuthGuard } from '../../core/guards/auth.guard';

const routes: Routes = [
    { path: '', component: MyProfilePageComponent, canActivate: [AuthGuard] },   
  ];
  
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProfileRoutingModule { }