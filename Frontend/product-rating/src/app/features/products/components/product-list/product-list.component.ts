import { Component, Input, Output, EventEmitter } from '@angular/core';
import { ProductCellData } from 'src/app/models/products/ProductCellData';
import { SearchState } from 'src/app/store/search-store/search.state';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent {

  @Input()
  products: ProductCellData[] = [];
  @Input()
  pageSize: number;
  @Input()
  totalNumOfResults: number;

  @Output()
  onPageChange: EventEmitter<number> = new EventEmitter<number>();

  pageChange(page: number) {
    this.onPageChange.emit(page);
  }
}
