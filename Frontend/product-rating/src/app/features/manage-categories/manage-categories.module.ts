import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewCategoryComponent } from './pages/new-category/new-category.component';
import { MyCategoriesComponent } from './pages/my-categories/my-categories.component';
import { ManageCategoriesRoutingModule } from './manage-categories-routing.module';
import { AttributeComponent } from './components/attribute/attribute.component';
import { EditCategoryComponent } from './pages/edit-category/edit-category.component';
import { CategoryFormComponent } from './components/category-form/category-form.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [
    NewCategoryComponent,
    MyCategoriesComponent,
    AttributeComponent,
    EditCategoryComponent,
    CategoryFormComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ManageCategoriesRoutingModule
  ]
})
export class ManageCategoriesModule { }
