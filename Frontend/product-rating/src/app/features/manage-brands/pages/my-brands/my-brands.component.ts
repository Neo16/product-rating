import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { BrandManageHeaderData } from 'src/app/models/brands/BrandManageHeaderData';
import { ManageBrandFilterData } from 'src/app/models/brands/ManageBrandFilterData';
import { PaginationParams } from 'src/app/models/search/PaginationParams';
import { ManageBrandsService } from '../../services/manage-brands-service';
import { ModalService } from 'src/app/shared/services/modal-service';

@Component({
  selector: 'app-my-brands',
  templateUrl: './my-brands.component.html',
  styleUrls: ['./my-brands.component.scss']
})
export class MyBrandsComponent implements OnInit {

  @ViewChild('editTmpl') editTmpl: TemplateRef<any>;
  @ViewChild('hdrTpl') hdrTpl: TemplateRef<any>;

  @ViewChild('deleteTmpl') deleteTmpl: TemplateRef<any>;

  filter: ManageBrandFilterData = new ManageBrandFilterData();
  pagination: PaginationParams = new PaginationParams();
  brands: BrandManageHeaderData[];

  columns = [];

  constructor(
    private manageBrandService: ManageBrandsService,
    private modalService: ModalService) { }

  listBrands() {
    this.pagination.length = 10;
    this.pagination.start = 1;

    this.manageBrandService.getBrands(this.filter, this.pagination)
      .subscribe(result => {
        this.brands = result;
      })
  }

  ngOnInit() {
    this.columns = [
      { prop: 'name' },
      { prop: 'numOfProducts' },
      { prop: 'categories' },
      { name: 'id', cellTemplate: this.editTmpl, headerTemplate: this.hdrTpl, width: '100px' },
      { name: 'id', cellTemplate: this.deleteTmpl, headerTemplate: this.hdrTpl, width: '100px' }
    ];
    this.listBrands();
  }

  onToggleChange(isMine: boolean) {
    this.filter.isMine = isMine;
    this.reload();
  }

  reload() {
    this.listBrands();
  }

  delete(id: string) {
    this.modalService.openConfirmationModal("Confirm delete", "Are you sure, you want to delete this product?")
      .then(yep => {
        this.manageBrandService.deleteBrand(id)
          .subscribe(result => {
            this.reload();
          })
      })
  }

}
