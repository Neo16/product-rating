import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewCategoryComponent } from './pages/new-category/new-category.component';
import { MyCategoriesComponent } from './pages/my-categories/my-categories.component';
import { ManageCategoriesRoutingModule } from './manage-categories-routing.module';

@NgModule({
  declarations: [
    NewCategoryComponent,
    MyCategoriesComponent
  ],
  imports: [
    CommonModule,
    ManageCategoriesRoutingModule
  ]
})
export class ManageCategoriesModule { }
