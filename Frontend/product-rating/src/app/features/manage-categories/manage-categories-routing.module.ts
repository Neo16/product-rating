import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../core/guards/auth.guard';
import { NewCategoryComponent } from './pages/new-category/new-category.component';
import { MyCategoriesComponent } from './pages/my-categories/my-categories.component';
import { EditCategoryComponent } from './pages/edit-category/edit-category.component';

const routes: Routes = [
  { path: 'edit/:id', component: EditCategoryComponent},  
  { path: 'new', component: NewCategoryComponent},  
  { path: '', component: MyCategoriesComponent} 
];
  
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ManageCategoriesRoutingModule { }