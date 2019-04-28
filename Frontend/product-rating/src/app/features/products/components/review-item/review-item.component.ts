import { Component, OnInit, Input } from '@angular/core';
import { ReviewData } from 'src/app/models/reviews/ReviewData';

@Component({
  selector: 'app-review-item',
  templateUrl: './review-item.component.html',
  styleUrls: ['./review-item.component.scss']
})
export class ReviewItemComponent implements OnInit {

  constructor() { }

  @Input() review: ReviewData; 

  ngOnInit() {
  }

}
