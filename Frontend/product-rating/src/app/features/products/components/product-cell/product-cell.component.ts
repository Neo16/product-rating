import { Component, OnInit, Input } from '@angular/core';
import { ProductCellData } from 'src/app/models/products/ProductCellData';

@Component({
  selector: 'app-product-cell',
  templateUrl: './product-cell.component.html',
  styleUrls: ['./product-cell.component.scss']
})
export class ProductCellComponent implements OnInit {

  @Input() product: ProductCellData;
  
  constructor() { }

  ngOnInit() {
   
  }

}
