import { Component, OnInit, Input } from '@angular/core';
import { CreateEditCategoryAttributeData } from 'src/app/models/categories/CreateEditCategoryAttributeData';

@Component({
  selector: 'app-attribute',
  templateUrl: './attribute.component.html',
  styleUrls: ['./attribute.component.scss']
})
export class AttributeComponent implements OnInit {

  @Input()
  attribute: CreateEditCategoryAttributeData;

  constructor() { }

  ngOnInit() {
  }

}
