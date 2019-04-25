import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../core/guards/auth.guard';
import { ProductDetailsComponent } from './pages/product-details/product-details.component';

const routes: Routes = [
    { path: ':id', component: ProductDetailsComponent },   
  ];
  
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ProductRoutingModule { }