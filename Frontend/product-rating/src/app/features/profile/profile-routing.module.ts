import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProfilePageComponent } from './pages/profile/profile.page.component';
import { AuthGuard } from '../../core/guards/auth.guard';

const routes: Routes = [
  {
    path: '', component: ProfilePageComponent, canActivate: [AuthGuard], data: {
      expectedRoles: ['ADMIN', 'SHOP_OWNER', 'USER']
    }
  },
  {
    path: ':id', component: ProfilePageComponent, canActivate: [AuthGuard], data: {
      expectedRoles: ['ADMIN', 'SHOP_OWNER', 'USER']
    }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProfileRoutingModule { }