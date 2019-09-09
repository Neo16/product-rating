import { Component, OnInit } from '@angular/core';
import { CreateEditBrandData } from 'src/app/models/brands/CreateEditBrandData';
import { BrandFormComponent } from '../../components/brand-form/brand-form.component';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { ManageBrandsService } from '../../services/manage-brands-service';

@Component({
  selector: 'app-edit-brand',
  templateUrl: './edit-brand.component.html',
  styleUrls: ['./edit-brand.component.scss']
})
export class EditBrandComponent implements OnInit {

  id: string;
  brand: CreateEditBrandData;

  constructor(
    private manageBrandsService: ManageBrandsService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    this.route.paramMap.subscribe((params: ParamMap) => {
      this.id = params.get('id');
    });

    this.manageBrandsService.getBrand(this.id)
      .subscribe(result => {      
        this.brand = result;
      })
  }

  onSubmit() {
    this.manageBrandsService.updateBrand(this.id, this.brand)
      .subscribe(e => {
        this.router.navigate(['manage-brands']);
      })
  }
}
