import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ManageBrandsRoutingModule } from './manage-brands-routing.module';
import { MyBrandsComponent } from './pages/my-brands/my-brands.component';
import { EditBrandComponent } from './pages/edit-brand/edit-brand.component';
import { NewBrandComponent } from './pages/new-brand/new-brand.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from 'src/app/core/services/token.service';
import { ManageBrandsService } from './services/manage-brands-service';
import { SharedModule } from 'src/app/shared/shared.module';
import { BrandFormComponent } from './components/brand-form/brand-form.component';

@NgModule({
  declarations: [MyBrandsComponent, EditBrandComponent, NewBrandComponent, BrandFormComponent],
  imports: [
    CommonModule,
    SharedModule,
    ManageBrandsRoutingModule
  ],
  providers:[
    ManageBrandsService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
  ]
})
export class ManageBrandsModule { }
