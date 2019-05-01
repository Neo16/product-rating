import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../core/guards/auth.guard';
import { MainPageComponent } from './pages/main.page/main.page.component';

const routes: Routes = [
    { path: '', component: MainPageComponent, canActivate: [AuthGuard]  },   
  ];
  
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class SearchRoutingModule { }