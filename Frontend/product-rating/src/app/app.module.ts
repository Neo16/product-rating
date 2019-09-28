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
import { GlobalErrorHandler } from './global-error-handler';
import { ManageBrandsModule } from './features/manage-brands/manage-brands.module';
import { PopupComponent } from './shared/popup/popup.component';
import { ManageUsersModule } from './features/manage-users/manage-users.module';

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
    ManageUsersModule,
    StoreModule.forRoot(reducers, {}),
    EffectsModule.forRoot([AcccountEffects, SearchEffects])
  ],
  providers: [{provide: ErrorHandler, useClass: GlobalErrorHandler}],
  bootstrap: [AppComponent],
  entryComponents: [PopupComponent]
})
export class AppModule { }
