import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EditBrandComponent } from './pages/edit-brand/edit-brand.component';
import { MyBrandsComponent } from './pages/my-brands/my-brands.component';
import { NewBrandComponent } from './pages/new-brand/new-brand.component';
import { AuthGuard } from 'src/app/core/guards/auth.guard';

const routes: Routes = [
  { path: 'edit/:id', component: EditBrandComponent, canActivate: [AuthGuard]},  
  { path: 'new', component: NewBrandComponent, canActivate: [AuthGuard]},  
  { path: '', component: MyBrandsComponent,canActivate: [AuthGuard]} 
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ManageBrandsRoutingModule { }
