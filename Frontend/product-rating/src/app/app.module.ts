import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { HttpClientModule } from '@angular/common/http';
import { SearchModule } from './features/search/search.module';
import { ProductsModule } from './features/products/products.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { reducers } from './store/root-state';
import { AcccountEffects } from './store/account-store/account.effects';
import { SearchEffects } from './store/search-store/search.effects';
import { ManageProductsModule } from './features/manage-products/manage-products.module';
import { ManageCategoriesModule } from './features/manage-categories/manage-categories.module';
import { LoggingService } from './core/services/logger.service';
import { GlobalErrorHandler } from './global-error-handler';
import { ManageBrandsModule } from './features/manage-brands/manage-brands.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CoreModule,
    SearchModule,
    HttpClientModule,
    ProductsModule,
    ManageProductsModule,
    ManageCategoriesModule,
    ManageBrandsModule,
    BrowserAnimationsModule,
    StoreModule.forRoot(reducers, {}),
    EffectsModule.forRoot([AcccountEffects, SearchEffects])
  ],
  providers: [{provide: ErrorHandler, useClass: GlobalErrorHandler}],
  bootstrap: [AppComponent]
})
export class AppModule { }
