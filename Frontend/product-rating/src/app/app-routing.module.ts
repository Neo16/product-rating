import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './core/components/layout/layout.component';
import { CoreModule } from './core/core.module';
import { ProfileModule } from './features/profile/profile.module';
import { SearchModule } from './features/search/search.module';
import { ProductsModule } from './features/products/products.module';

const routes: Routes = [
  {
    path: 'account',
    component: LayoutComponent,
    loadChildren: () => CoreModule
  },
  {
    path: 'profile',
    component: LayoutComponent,
    loadChildren: () => ProfileModule
  },
  {
    path: 'products',
    component: LayoutComponent,
    loadChildren: () => ProductsModule
  },
  {
    path: 'search',
    component: LayoutComponent,
    loadChildren: () => SearchModule
  },
  {
    path: '',
    redirectTo: '/search',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
