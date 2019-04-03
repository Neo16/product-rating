import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuComponent } from './components/menu/menu.component';
import { LayoutComponent } from './components/layout/layout.component';
import { AuthGuard } from './guards/auth.guard';
import { RouterModule } from '@angular/router';
import { CoreRoutingModule } from './core-routing.module';
import { LoginPageComponent } from './pages/login/login.page.component';
import { FormsModule } from '@angular/forms';
import { AccountService } from './services/account.service';
import { AcccountEffects } from '../store/account-store/account.effects';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { reducers } from '../store/root-state';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './services/token.service';

@NgModule({
  declarations: [
    LoginPageComponent,
    MenuComponent,
    LayoutComponent    
  ],
  imports: [
    CommonModule,
    CoreRoutingModule,
    FormsModule,
    StoreModule.forRoot(reducers, {}),
    EffectsModule.forRoot([AcccountEffects]),    
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