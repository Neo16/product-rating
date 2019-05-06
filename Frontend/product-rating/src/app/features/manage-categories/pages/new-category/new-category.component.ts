import { Component, OnInit } from '@angular/core';
import { CreateEditCategoryData } from 'src/app/models/categories/CreateEditCategoryData';
import { ManageCategoriesService } from '../../services/manage-categories.service';

@Component({
  selector: 'app-new-category',
  templateUrl: './new-category.component.html',
  styleUrls: ['./new-category.component.scss']
})
export class NewCategoryComponent implements OnInit {

  category: CreateEditCategoryData = new CreateEditCategoryData();
  
  constructor(private manageCatService: ManageCategoriesService) { }

  ngOnInit() { }

  onSubmit(){
    this.manageCatService.createCategory(this.category)
      .subscribe(e => {
         console.log(e);
      })
   }
}
