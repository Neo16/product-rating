import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { CategoryHeader } from 'src/app/models/search/CategoryHeader';

@Component({
  selector: 'app-category-picker',
  templateUrl: './category-picker.component.html',
  styleUrls: ['./category-picker.component.scss']
})
export class CategoryPickerComponent implements OnInit {

  @Input() category: CategoryHeader;
  @Output() select: EventEmitter<string> = new EventEmitter();
  @Output() unSelect: EventEmitter<any> = new EventEmitter();

  constructor() { }

  selectCategory(categoryId: string){   
    this.select.emit(categoryId);
  }

  unSelectCategory(categoryId: string){ 
    this.unSelect.emit(categoryId);
  }

  ngOnInit() {
  }

}
