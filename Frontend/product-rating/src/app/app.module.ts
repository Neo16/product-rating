import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { EffectsModule } from '@ngrx/effects';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { AcccountEffects } from './store/account-store/account.effects';
import { HttpClientModule } from '@angular/common/http';
import { SearchModule } from './features/search/search.module';
import { ProductsModule } from './features/products/products.module';
import { RootStoreModule } from './store/root-store.module';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    NgbModule,
    AppRoutingModule,
    CoreModule,
    SearchModule,
    HttpClientModule,
    ProductsModule   
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
