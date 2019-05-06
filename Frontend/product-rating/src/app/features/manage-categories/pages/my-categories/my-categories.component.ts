import { Component, OnInit } from '@angular/core';
import { CategoryManageHeaderData } from 'src/app/models/categories/CategoryManageHeaderData';
import { ManageCategoryFilterData } from 'src/app/models/categories/ManageCategoryFilterData';
import { ManageCategoriesService } from '../../services/manage-categories.service';
import { PaginationParams } from 'src/app/models/search/PaginationParams';

@Component({
  selector: 'app-my-categories',
  templateUrl: './my-categories.component.html',
  styleUrls: ['./my-categories.component.scss']
})
export class MyCategoriesComponent implements OnInit {

  filter: ManageCategoryFilterData = new ManageCategoryFilterData();
  pagination: PaginationParams = new PaginationParams();
  categories: CategoryManageHeaderData[];
  
  columns = [
    { prop: 'name' },
    { prop: 'numOfProducts' },
    { prop: 'attributeNames' },
    { prop: 'parentName' }
  ];

  constructor(private manageCatService: ManageCategoriesService) { }

  listCategories() {
    this.pagination.length = 10;
    this.pagination.start = 1;

    this.manageCatService.getCategories(this.filter, this.pagination)
      .subscribe(result => {
        console.log(result);
        this.categories = result;
      })
  }

  ngOnInit() {
    this.listCategories();
  }
}
