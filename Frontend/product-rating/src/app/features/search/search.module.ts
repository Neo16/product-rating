import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MainPageComponent } from './pages/main.page/main.page.component';
import { SearchBarComponent } from './components/search-bar/search-bar.component';
import { SearchRoutingModule } from './search-routing.module';
import { PropertySearchComponent } from './components/property-search/property-search.component';
import { ProductsModule } from '../products/products.module';

@NgModule({
  declarations: [
    MainPageComponent,
    SearchBarComponent,
    PropertySearchComponent
  ],
  imports: [
    CommonModule,
    SearchRoutingModule,
    ProductsModule
  ]
})
export class SearchModule { }
