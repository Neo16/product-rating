import { Component, OnInit } from '@angular/core';
import { CreateEditProductData } from 'src/app/models/products/CreateEditProductData';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { ManageProductsService } from '../../services/manage-products.service';
import { PictureData } from 'src/app/models/PictureData';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.scss']
})
export class EditProductComponent implements OnInit {

  id: string;
  product: CreateEditProductData;
  

  constructor( 
    private manageProductService: ManageProductsService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    this.route.paramMap.subscribe((params: ParamMap) => {
      this.id = params.get('id');
    });

    this.manageProductService.getProduct(this.id)
      .subscribe(result => {
        this.product = result;

        if (this.product.thumbnailPicture == null){
          this.product.thumbnailPicture = new PictureData();
        }
      })
  }

  onSubmit() {  
    this.manageProductService.updateProduct(this.product.id, this.product)
      .subscribe(e => {
        this.router.navigate(['manage-products']);
      })
  }
}
