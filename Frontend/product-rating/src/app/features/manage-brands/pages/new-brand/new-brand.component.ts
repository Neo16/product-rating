import { Component, OnInit } from '@angular/core';
import { CreateEditBrandData } from 'src/app/models/brands/CreateEditBrandData';
import { ManageBrandsService } from '../../services/manage-brands-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-new-brand',
  templateUrl: './new-brand.component.html',
  styleUrls: ['./new-brand.component.scss']
})
export class NewBrandComponent implements OnInit {

  brand: CreateEditBrandData = new CreateEditBrandData();

  constructor(
    private manageBrandsService: ManageBrandsService,   
    private router: Router
  ) { }

  ngOnInit() {
  }

  onSubmit() {
    this.manageBrandsService.createBrand(this.brand)
      .subscribe(e => {
        this.router.navigate(['manage-brands']);
      })
  }
}
