import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
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

  @Output()
  delete: EventEmitter<CreateEditCategoryAttributeData> = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

  addValue(){
    this.attribute.values.push(new CreateEditCategoryAttributeValueData()); 
  }

  remove(){
    this.delete.emit(this.attribute);
  }

  onValueDelete(value: CreateEditCategoryAttributeValueData){
    this.attribute.values = this.attribute.values
      .filter(item => item !== value);
  } 
}
