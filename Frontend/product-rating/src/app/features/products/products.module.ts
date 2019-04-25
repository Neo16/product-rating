import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductListComponent } from './components/product-list/product-list.component';
import { ProductCellComponent } from './components/product-cell/product-cell.component';
import { ProductService } from './services/product.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from 'src/app/core/services/token.service';
import { ProductDetailsComponent } from './pages/product-details/product-details.component';
import { ProductRoutingModule } from './products-routing.module';

@NgModule({
  declarations: [
    ProductListComponent,
    ProductCellComponent,
    ProductDetailsComponent    
  ],
  imports: [
    CommonModule,
    ProductRoutingModule
  ],
  exports: [ProductListComponent],
  providers: [
    ProductService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
  ]
})
export class ProductsModule { }
