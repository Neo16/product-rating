import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../core/guards/auth.guard';
import { NewProductComponent } from './pages/new-product/new-product.component';
import { MyProductsComponent } from './pages/my-products/my-products.component';
import { EditProductComponent } from './pages/edit-product/edit-product.component';

const routes: Routes = [
  { path: 'edit/:id', component: EditProductComponent, canActivate: [AuthGuard], data: { expectedRoles: ['SHOP_OWNER']} },
  { path: 'new', component: NewProductComponent, canActivate: [AuthGuard], data: { expectedRoles: ['SHOP_OWNER']} },
  { path: '', component: MyProductsComponent, canActivate: [AuthGuard], data: { expectedRoles: ['SHOP_OWNER']} }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ManageProductsRoutingModule { }