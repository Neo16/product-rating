import { Component, OnInit } from '@angular/core';
import { CreateEditProductData } from 'src/app/models/products/CreateEditProductData';
import { Router } from '@angular/router';
import { ManageProductsService } from '../../services/manage-products.service';

@Component({
  selector: 'app-new-product',
  templateUrl: './new-product.component.html',
  styleUrls: ['./new-product.component.scss']
})
export class NewProductComponent implements OnInit {

  product: CreateEditProductData = new CreateEditProductData();

  constructor(
    private manageCatService: ManageProductsService,
    private router: Router) { }

  ngOnInit() { }

  onSubmit(){
    this.manageCatService.createProduct(this.product)
      .subscribe(e => {
         this.router.navigate(['manage-products']);
      })
   }
}
