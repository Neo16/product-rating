import { Component, OnInit, Input } from '@angular/core';
import { CreateEditCategoryData } from 'src/app/models/categories/CreateEditCategoryData';
import { CreateEditCategoryAttributeData } from 'src/app/models/categories/CreateEditCategoryAttributeData';

@Component({
  selector: 'app-category-form',
  templateUrl: './category-form.component.html',
  styleUrls: ['./category-form.component.scss']
})
export class CategoryFormComponent implements OnInit {
  
  @Input()
  category: CreateEditCategoryData;

  constructor() { }

  ngOnInit() {
  }

  addNewAttribute(){
    this.category.attributes.push(new CreateEditCategoryAttributeData()); 
  }

  onAttributeDelete(attr: CreateEditCategoryAttributeData){
    this.category.attributes = this.category.attributes
        .filter(item => item !== attr);
  }
}
