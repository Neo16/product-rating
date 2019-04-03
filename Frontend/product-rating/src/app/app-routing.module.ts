import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './core/components/layout/layout.component';
import { CoreModule } from './core/core.module';
import { ProfileModule } from './features/profile/profile.module';
import { SearchModule } from './features/search/search.module';

const routes: Routes = [
  {
    path: 'account',
    component: LayoutComponent,
    loadChildren: () => CoreModule
  },
  {
    path: '',
    component: LayoutComponent,
    loadChildren: () => SearchModule
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