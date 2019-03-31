import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuComponent } from './components/menu/menu.component';
import { LayoutComponent } from './components/layout/layout.component';
import { AuthGuard } from './guards/auth.guard';
import { RouterModule } from '@angular/router';
import { CoreRoutingModule } from './core-routing.module';
import { LoginPageComponent } from './pages/login/login.page.component';

@NgModule({
  declarations: [
    LoginPageComponent,
    MenuComponent,
    LayoutComponent
  ],
  imports: [
    CommonModule,
    CoreRoutingModule
  ],
  providers: [   
    AuthGuard
  ],  
})
export class CoreModule { }
