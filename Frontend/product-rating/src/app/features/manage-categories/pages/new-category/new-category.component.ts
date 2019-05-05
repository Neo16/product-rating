import { Component, OnInit } from '@angular/core';
import { CreateEditCategoryData } from 'src/app/models/categories/CreateEditCategoryData';

@Component({
  selector: 'app-new-category',
  templateUrl: './new-category.component.html',
  styleUrls: ['./new-category.component.scss']
})
export class NewCategoryComponent implements OnInit {

  category: CreateEditCategoryData = new CreateEditCategoryData();
  
  constructor() { }

  ngOnInit() {
   
  }
}
