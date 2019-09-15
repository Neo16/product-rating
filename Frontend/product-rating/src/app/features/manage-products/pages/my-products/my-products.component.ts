import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { PaginationParams } from 'src/app/models/search/PaginationParams';
import { ProductService } from 'src/app/features/products/services/product.service';
import { ManageProductFilterData } from 'src/app/models/products/ManageProductFilterData';
import { ProductManageHeaderData } from 'src/app/models/products/ProductHeaderData';
import { ManageProductsService } from '../../services/manage-products.service';
import { ModalService } from 'src/app/shared/services/modal-service';

@Component({
  selector: 'app-my-products',
  templateUrl: './my-products.component.html',
  styleUrls: ['./my-products.component.scss']
})
export class MyProductsComponent implements OnInit {

  @ViewChild('editTmpl') editTmpl: TemplateRef<any>;
  @ViewChild('hdrTpl') hdrTpl: TemplateRef<any>;

  @ViewChild('deleteTmpl') deleteTmpl: TemplateRef<any>;

  filter: ManageProductFilterData = new ManageProductFilterData();
  pagination: PaginationParams = new PaginationParams();
  products: ProductManageHeaderData[];

  columns = [];

  constructor(private manageProductService: ManageProductsService,
    private modalService: ModalService) { }

  listCategories() {
    //Todo: use server side paging.
    this.pagination.length = 1000;
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
      { name: 'id', cellTemplate: this.editTmpl, headerTemplate: this.hdrTpl },
      { name: 'id', cellTemplate: this.deleteTmpl, headerTemplate: this.hdrTpl }
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

  delete(id: string) {
    this.modalService.openConfirmationModal("Confirm delete", "Are you sure, you want to delete this product?")
      .then(yep => {
        this.manageProductService.deleteProduct(id)
          .subscribe(result => {
            this.reload();
          });
      })
  }
}
