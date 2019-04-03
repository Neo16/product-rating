import { Component, OnInit } from '@angular/core';
import { ProductOrder, ProductOrderDisplay } from 'src/app/models/ProductOrder';
import { Order, OrderDisplay } from 'src/app/models/Order';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.scss']
})
export class SearchBarComponent implements OnInit {

  productOrder = ProductOrder;
  productOrderDisplay = ProductOrderDisplay;

  order = Order;
  orderDisplay = OrderDisplay;

  constructor() { }

  ngOnInit() {
  } 

}
