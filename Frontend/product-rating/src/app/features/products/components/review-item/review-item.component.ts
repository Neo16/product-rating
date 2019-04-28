import { Component, OnInit, Input } from '@angular/core';
import { ReviewData } from 'src/app/models/reviews/ReviewData';
import { ReviewService } from '../../services/review.service';

@Component({
  selector: 'app-review-item',
  templateUrl: './review-item.component.html',
  styleUrls: ['./review-item.component.scss']
})
export class ReviewItemComponent implements OnInit {

  constructor(private reviewService: ReviewService) { }

  @Input() review: ReviewData; 

  ngOnInit() {
  }

  upvote(){
     this.reviewService.upvoteReview(this.review.id)
      .subscribe(e =>{ this.review.points++; })    
  }

  downvote(){
    this.reviewService.upvoteReview(this.review.id)
     .subscribe(e =>{ this.review.points--; })     
  }

}
