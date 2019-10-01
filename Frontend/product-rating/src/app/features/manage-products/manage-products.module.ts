import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewProductComponent } from './pages/new-product/new-product.component';
import { MyProductsComponent } from './pages/my-products/my-products.component';
import { ManageProductsRoutingModule } from './manage-products-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { EditProductComponent } from './pages/edit-product/edit-product.component';
import { ProductFormComponent } from './components/product-form/product-form.component';
import { PictureUploaderComponent } from './components/picture-uploader/picture-uploader.component';
import { ManageProductsService } from './services/manage-products.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from 'src/app/core/services/token.service';
import { PictureService } from './services/picture-service';
import { OfferFormComponent } from './components/offer-form/offer-form.component';

@NgModule({
  declarations: [
    NewProductComponent,
    MyProductsComponent,
    EditProductComponent,
    ProductFormComponent,
    PictureUploaderComponent,
    OfferFormComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ManageProductsRoutingModule
  ],
  providers: [
    ManageProductsService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
    PictureService,    
  ]
})
export class ManageProductsModule { }
