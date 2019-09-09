import { Component, OnInit } from '@angular/core';
import { CreateEditCategoryData } from 'src/app/models/categories/CreateEditCategoryData';
import { ManageCategoriesService } from '../../services/manage-categories.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-new-category',
  templateUrl: './new-category.component.html',
  styleUrls: ['./new-category.component.scss']
})
export class NewCategoryComponent implements OnInit {

  category: CreateEditCategoryData = new CreateEditCategoryData();
  
  constructor(
    private manageCatService: ManageCategoriesService,
    private router: Router) { }

  ngOnInit() { }

  onSubmit(){
    this.manageCatService.createCategory(this.category)
      .subscribe(e => {
         this.router.navigate(['manage-categories']);
      })
   }
}
