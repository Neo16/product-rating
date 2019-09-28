import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MainPageComponent } from './pages/main.page/main.page.component';
import { SearchBarComponent } from './components/search-bar/search-bar.component';
import { SearchRoutingModule } from './search-routing.module';
import { PropertySearchComponent } from './components/property-search/property-search.component';
import { ProductsModule } from '../products/products.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { HelperService } from './services/search-helper.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from 'src/app/core/services/token.service';
import { CategoryPickerComponent } from './components/category-picker/category-picker.component';

@NgModule({
  declarations: [
    MainPageComponent,
    SearchBarComponent,
    PropertySearchComponent,
    CategoryPickerComponent
  ],
  imports: [
    SharedModule,
    CommonModule,
    SearchRoutingModule,
    ProductsModule,
    SharedModule
  ],
  providers: [
    HelperService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
  ]
})
export class SearchModule { }
