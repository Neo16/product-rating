import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './core/components/layout/layout.component';
import { CoreModule } from './core/core.module';
import { ProfileModule } from './profile/profile.module';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    loadChildren: () => CoreModule
  },
  {
    path: 'profile',
    component: LayoutComponent,
    loadChildren: () => ProfileModule    
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
