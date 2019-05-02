import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../core/guards/auth.guard';
import { NewProductComponent } from './pages/new-product/new-product.component';
import { MyProductsComponent } from './pages/my-products/my-products.component';

const routes: Routes = [
    { path: 'new', component: NewProductComponent, canActivate: [AuthGuard] },  
    { path: '', component: MyProductsComponent, canActivate: [AuthGuard] } 
  ];
  
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ManageProductsRoutingModule { }