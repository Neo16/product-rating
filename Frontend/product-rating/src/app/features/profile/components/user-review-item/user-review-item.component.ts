import { Component, OnInit, Input } from '@angular/core';
import { TextReviewWithProductInfoData } from 'src/app/models/profile/TextReviewWithProductInfoData';

@Component({
  selector: 'app-user-review-item',
  templateUrl: './user-review-item.component.html',
  styleUrls: ['./user-review-item.component.scss']
})
export class UserReviewItemComponent {

  @Input()
  review: TextReviewWithProductInfoData;

  constructor() { }

}
