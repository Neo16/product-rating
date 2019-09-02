import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ManageBrandsRoutingModule } from './manage-brands-routing.module';
import { MyBrandsComponent } from './my-brands/my-brands.component';
import { EditBrandComponent } from './edit-brand/edit-brand.component';
import { NewBrandComponent } from './new-brand/new-brand.component';

@NgModule({
  declarations: [MyBrandsComponent, EditBrandComponent, NewBrandComponent],
  imports: [
    CommonModule,
    ManageBrandsRoutingModule
  ]
})
export class ManageBrandsModule { }
