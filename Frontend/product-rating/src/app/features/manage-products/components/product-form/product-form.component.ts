import { Component, OnInit, Input } from '@angular/core';
import { CreateEditProductData } from 'src/app/models/products/CreateEditProductData';
import { ManageCategoriesService } from 'src/app/features/manage-categories/services/manage-categories.service';
import { CategoryManageHeaderData } from 'src/app/models/categories/CategoryManageHeaderData';
import { ManageCategoryFilterData } from 'src/app/models/categories/ManageCategoryFilterData';
import { PaginationParams } from 'src/app/models/search/PaginationParams';
import { PictureService } from '../../services/picture-service';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.scss']
})
export class ProductFormComponent implements OnInit {

  @Input()
  product: CreateEditProductData;  
  categories: CategoryManageHeaderData[]; 
 

  constructor(
    private manageCategoriesService: ManageCategoriesService,
    private pictureservice: PictureService) {    
  }

  processFile(imageInput: any) {
    const file: File = imageInput.files[0];
    const reader = new FileReader();

    reader.addEventListener('load', (event: any) => {    
      this.pictureservice.uploadImage(file).subscribe(
        (res) => {        
          alert("OK");
          console.log(res);
        },
        (err) => {
          alert("Not OK");
          console.log(err);
        })
    });

    reader.readAsDataURL(file);
  }


  ngOnInit() {
    this.manageCategoriesService
      .getCategories(new ManageCategoryFilterData() , new PaginationParams())
      .subscribe(result =>{
          this.categories = result;
      })
  }

}
