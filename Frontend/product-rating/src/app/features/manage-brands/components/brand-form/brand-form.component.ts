import { Component, OnInit, Input } from '@angular/core';
import { CreateEditBrandData } from 'src/app/models/brands/CreateEditBrandData';

@Component({
  selector: 'app-brand-form',
  templateUrl: './brand-form.component.html',
  styleUrls: ['./brand-form.component.scss']
})
export class BrandFormComponent implements OnInit {
  
  @Input()
  brand: CreateEditBrandData;

  constructor() { }

  ngOnInit() {
  }

}
