import { Component, OnInit, Input } from '@angular/core';
import { CreateEditCategoryAttributeValueData } from 'src/app/models/categories/CreateEditCategoryAttributeValueData';

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

  constructor() { }

  ngOnInit() {
  }

}
