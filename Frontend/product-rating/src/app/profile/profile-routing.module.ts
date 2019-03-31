import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../core/guards/auth.guard';
import { MyProfilePageComponent } from './pages/my-profile/my-profile.page.component';

const routes: Routes = [
    { path: '', component: MyProfilePageComponent, canActivate: [AuthGuard] },   
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })
  export class ProfileRoutingModule { }