import { Component, OnInit, Input } from '@angular/core';
import { CreateEditProductData } from 'src/app/models/products/CreateEditProductData';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.scss']
})
export class ProductFormComponent implements OnInit {

  @Input()
  product: CreateEditProductData;

  constructor() { }

  ngOnInit() {
  }

}
