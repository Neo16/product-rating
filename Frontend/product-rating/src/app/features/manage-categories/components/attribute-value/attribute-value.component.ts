import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { CreateEditCategoryAttributeValueData } from 'src/app/models/categories/CreateEditCategoryAttributeValueData';
import { AttributeType } from 'src/app/models/categories/AttributeType';

@Component({
  selector: 'app-attribute-value',
  templateUrl: './attribute-value.component.html',
  styleUrls: ['./attribute-value.component.scss']
})
export class AttributeValueComponent implements OnInit {

  @Input()
  attributeValue: CreateEditCategoryAttributeValueData;
  @Input()
  index: number;
  @Input()
  type: AttributeType;

  @Output()
  delete: EventEmitter<CreateEditCategoryAttributeValueData> = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

  remove(){
    this.delete.emit(this.attributeValue);
  }
}
