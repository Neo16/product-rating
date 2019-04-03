import { Component, OnInit } from '@angular/core';
import { ProductCellData } from 'src/app/models/ProductCellData';
import { ProductService } from '../../services/product.service';
import { Store } from '@ngrx/store';
import { SearchState } from 'src/app/store/search-store/search.state';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {

  productCells: ProductCellData[] = []; 

  constructor(
    private productService: ProductService,
    private store: Store<SearchState>
  ) { }

  ngOnInit() {  
    this.productService.searchProducts({})
      .subscribe(result => {       
        console.log(result)
        this.productCells = result;      
      }); 
  }
}
