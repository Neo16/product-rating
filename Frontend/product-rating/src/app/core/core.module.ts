import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuComponent } from './components/menu/menu.component';
import { LayoutComponent } from './components/layout/layout.component';
import { AuthGuard } from './guards/auth.guard';
import { CoreRoutingModule } from './core-routing.module';
import { LoginPageComponent } from './pages/login/login.page.component';
import { AccountService } from './services/account.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './services/token.service';
import { SharedModule } from '../shared/shared.module';
import { RegisterComponent } from './pages/register/register.component';

@NgModule({
  declarations: [
    LoginPageComponent,
    MenuComponent,
    LayoutComponent,
    RegisterComponent
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
