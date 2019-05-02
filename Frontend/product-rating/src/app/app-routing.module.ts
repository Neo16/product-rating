import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './core/components/layout/layout.component';
import { CoreModule } from './core/core.module';
import { ProfileModule } from './features/profile/profile.module';
import { SearchModule } from './features/search/search.module';
import { ProductsModule } from './features/products/products.module';
import { ManageProductsModule } from './features/manage-products/manage-products.module';
import { ManageCategoriesModule } from './features/manage-categories/manage-categories.module';

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
    path: 'manage-products',
    component: LayoutComponent,
    loadChildren: () => ManageProductsModule
  },
  {
    path: 'manage-categories',
    component: LayoutComponent,
    loadChildren: () => ManageCategoriesModule
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
