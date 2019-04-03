import { Component, OnInit } from '@angular/core';
import { ProductOrder, ProductOrderDisplay } from 'src/app/models/ProductOrder';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.scss']
})
export class SearchBarComponent implements OnInit {

  productOrders = ProductOrder;
  productOrderDisplay = ProductOrderDisplay;
  constructor() { }

  ngOnInit() {
  } 

}
