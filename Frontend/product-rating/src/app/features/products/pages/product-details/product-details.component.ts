import { Component, OnInit } from '@angular/core';
import { ProductDetailsData } from 'src/app/models/products/ProductDetailsData';
import { Router } from '@angular/router';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
 
  product: ProductDetailsData = null;

  constructor(
    private router: Router,
    private productSerive: ProductService
  ) { }

  ngOnInit() {    
    var productId = this.router.url.split(/[\/ ]+/).pop();

    this.productSerive.getProductDetails(productId)
      .subscribe(result => {
        this.product = result;
    });
  }
}
