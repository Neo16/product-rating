import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { CategoryHeader } from 'src/app/models/CategoryHeader';

@Component({
  selector: 'app-category-picker',
  templateUrl: './category-picker.component.html',
  styleUrls: ['./category-picker.component.scss']
})
export class CategoryPickerComponent implements OnInit {

  @Input() category: CategoryHeader;
  @Output() select: EventEmitter<string> = new EventEmitter();
  @Output() unSelect: EventEmitter<any> = new EventEmitter();

  active:boolean = false;

  constructor() { }

  selectCategory(categoryId: string){   
    this.active = true;
    this.select.emit(categoryId);
  }

  unSelectCategory(categoryId: string){ 
    this.active = false;
    this.unSelect.emit(categoryId);
  }

  ngOnInit() {
  }

}
