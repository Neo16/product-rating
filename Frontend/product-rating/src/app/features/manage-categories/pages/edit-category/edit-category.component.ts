import { Component, OnInit } from '@angular/core';
import { ManageCategoriesService } from '../../services/manage-categories.service';
import { CreateEditCategoryData } from 'src/app/models/categories/CreateEditCategoryData';
import { Router, ParamMap, ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.scss']
})
export class EditCategoryComponent implements OnInit {

  id: string;
  category: CreateEditCategoryData;

  constructor(
    private manageCategoriesService: ManageCategoriesService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    this.route.paramMap.subscribe((params: ParamMap) => {
      this.id = params.get('id');
    });

    this.manageCategoriesService.getCategory(this.id)
      .subscribe(result => {
        this.category = result;
      })
  }

  onSubmit() {
    this.manageCategoriesService.updateCategory(this.id, this.category)
      .subscribe(e => {
        this.router.navigate(['manage-categories']);
      })
  }
}
