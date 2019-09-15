import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { CategoryManageHeaderData } from 'src/app/models/categories/CategoryManageHeaderData';
import { ManageCategoryFilterData } from 'src/app/models/categories/ManageCategoryFilterData';
import { ManageCategoriesService } from '../../services/manage-categories.service';
import { PaginationParams } from 'src/app/models/search/PaginationParams';
import { ModalService } from 'src/app/shared/services/modal-service';

@Component({
  selector: 'app-my-categories',
  templateUrl: './my-categories.component.html',
  styleUrls: ['./my-categories.component.scss']
})
export class MyCategoriesComponent implements OnInit {

  @ViewChild('editTmpl') editTmpl: TemplateRef<any>;
  @ViewChild('hdrTpl') hdrTpl: TemplateRef<any>;

  @ViewChild('deleteTmpl') deleteTmpl: TemplateRef<any>;

  filter: ManageCategoryFilterData = new ManageCategoryFilterData();
  pagination: PaginationParams = new PaginationParams();
  categories: CategoryManageHeaderData[];

  columns = [];

  constructor(
    private manageCatService: ManageCategoriesService,
    private modalService: ModalService) { }

  listCategories() {
    this.pagination.length = 10;
    this.pagination.start = 1;

    this.manageCatService.getCategories(this.filter, this.pagination)
      .subscribe(result => {
        console.log(result);
        this.categories = result;
      })
  }

  ngOnInit() {
    this.columns = [
      { prop: 'name' },
      { prop: 'numOfProducts' },
      { prop: 'attributeNames' },
      { prop: 'parentName' },
      { name: 'id', cellTemplate: this.editTmpl, headerTemplate: this.hdrTpl, width: '100px' },
      { name: 'id', cellTemplate: this.deleteTmpl, headerTemplate: this.hdrTpl, width: '100px' }
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
        this.manageCatService.deleteCategory(id)
          .subscribe(result => {
            this.reload();
          })
      })
  }
}
