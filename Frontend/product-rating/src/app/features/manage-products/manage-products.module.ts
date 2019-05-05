import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewProductComponent } from './pages/new-product/new-product.component';
import { MyProductsComponent } from './pages/my-products/my-products.component';
import { ManageProductsRoutingModule } from './manage-products-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [
    NewProductComponent,
    MyProductsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ManageProductsRoutingModule
  ]
})
export class ManageProductsModule { }
