import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../core/guards/auth.guard';
import { NewProductComponent } from './pages/new-product/new-product.component';
import { MyProductsComponent } from './pages/my-products/my-products.component';
import { ManageProductsService } from './services/manage-products.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from 'src/app/core/services/token.service';
import { EditProductComponent } from './pages/edit-product/edit-product.component';

const routes: Routes = [
  { path: 'edit/:id', component: EditProductComponent, canActivate: [AuthGuard] },
  { path: 'new', component: NewProductComponent, canActivate: [AuthGuard] },
  { path: '', component: MyProductsComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [
    ManageProductsService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
  ]
})
export class ManageProductsRoutingModule { }