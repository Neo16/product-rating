import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { PaginationParams } from 'src/app/models/search/PaginationParams';
import { ProductService } from 'src/app/features/products/services/product.service';
import { ManageProductFilterData } from 'src/app/models/products/ManageProductFilterData';
import { ProductManageHeaderData } from 'src/app/models/products/ProductHeaderData';
import { ManageProductsService } from '../../services/manage-products.service';

@Component({
  selector: 'app-my-products',
  templateUrl: './my-products.component.html',
  styleUrls: ['./my-products.component.scss']
})
export class MyProductsComponent implements OnInit {

  @ViewChild('editTmpl') editTmpl: TemplateRef<any>;
  @ViewChild('hdrTpl') hdrTpl: TemplateRef<any>;

  filter: ManageProductFilterData = new ManageProductFilterData();
  pagination: PaginationParams = new PaginationParams();
  products: ProductManageHeaderData[];

  columns = [];

  constructor(private manageProductService : ManageProductsService) { }

  listCategories() {
    this.pagination.length = 10;
    this.pagination.start = 1;

    this.manageProductService.getProducts(this.filter, this.pagination)
      .subscribe(result => {
        console.log(result);
        this.products = result;
      })
  }

  ngOnInit() {
    this.columns = [
      { prop: 'name' },   
      { prop: 'brandName' },
      { prop: 'categoryName' },
      { prop: 'createdAt' },
      { name: 'id',  cellTemplate: this.editTmpl, headerTemplate: this.hdrTpl}
    ];
    this.listCategories();
  }

  onToggleChange(isMine: boolean) {
    this.filter.isMine = isMine;
    this.reload();
  }

  reload() {
    this.listCategories();
  } 

}
