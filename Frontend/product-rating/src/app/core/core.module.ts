import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuComponent } from './components/menu/menu.component';
import { LayoutComponent } from './components/layout/layout.component';
import { AuthGuard } from './guards/auth.guard';
import { RouterModule } from '@angular/router';
import { CoreRoutingModule } from './core-routing.module';
import { LoginPageComponent } from './pages/login/login.page.component';
import { AccountService } from './services/account.service';
import { AcccountEffects } from '../store/account-store/account.effects';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { reducers } from '../store/root-state';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './services/token.service';
import { SharedModule } from '../shared/shared.module';
import { SearchEffects } from '../store/search-store/search.effects';


@NgModule({
  declarations: [
    LoginPageComponent,
    MenuComponent,
    LayoutComponent    
  ],
  imports: [    
    SharedModule,
    CommonModule,
    CoreRoutingModule
  ],
  providers: [   
    AuthGuard,
    AccountService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
  ],  
})
export class CoreModule { }
