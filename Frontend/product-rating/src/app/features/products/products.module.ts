import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductListComponent } from './components/product-list/product-list.component';
import { ProductCellComponent } from './components/product-cell/product-cell.component';
import { ProductService } from './services/product.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from 'src/app/core/services/token.service';

@NgModule({
  declarations: [
    ProductListComponent,
    ProductCellComponent    
  ],
  imports: [
    CommonModule
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
