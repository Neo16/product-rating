import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { MenuComponent } from './menu/menu.component';
import { LayoutComponent } from './layout/layout.component';

@NgModule({
  declarations: [LoginComponent, MenuComponent, LayoutComponent],
  imports: [
    CommonModule
  ]
})
export class CoreModule { }
