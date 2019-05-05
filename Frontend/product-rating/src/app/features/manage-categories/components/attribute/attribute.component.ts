import { Component, OnInit, Input } from '@angular/core';
import { CreateEditCategoryAttributeData } from 'src/app/models/categories/CreateEditCategoryAttributeData';
import { AttributeType } from 'src/app/models/categories/AttributeType';
import { AttributeTypeDisplay } from 'src/app/models/categories/AttributeType';
import { CreateEditCategoryAttributeValueData } from 'src/app/models/categories/CreateEditCategoryAttributeValueData';

@Component({
  selector: 'app-attribute',
  templateUrl: './attribute.component.html',
  styleUrls: ['./attribute.component.scss']
})
export class AttributeComponent implements OnInit {

  @Input()
  attribute: CreateEditCategoryAttributeData;
  attributeTypeDisplay = AttributeTypeDisplay;
  attributeType = AttributeType;

  constructor() { }

  ngOnInit() {
  }

  addValue(){
    this.attribute.values.push(new CreateEditCategoryAttributeValueData()); 
  }
}
