import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewProductComponent } from './pages/new-product/new-product.component';
import { MyProductsComponent } from './pages/my-products/my-products.component';
import { ManageProductsRoutingModule } from './manage-products-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { EditProductComponent } from './pages/edit-product/edit-product.component';
import { ProductFormComponent } from './components/product-form/product-form.component';

@NgModule({
  declarations: [
    NewProductComponent,
    MyProductsComponent,
    EditProductComponent,
    ProductFormComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ManageProductsRoutingModule
  ]
})
export class ManageProductsModule { }
