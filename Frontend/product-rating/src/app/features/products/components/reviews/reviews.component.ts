import { Component, OnInit, Input } from '@angular/core';
import { ReviewData } from 'src/app/models/reviews/ReviewData';
import { CreateReviewData } from 'src/app/models/reviews/CreateReviewData';
import { ReviewService } from '../../services/review.service';
import { ReviewMood } from 'src/app/models/reviews/ReviewMood';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.scss']
})
export class ReviewsComponent implements OnInit {

  constructor(private reviewService: ReviewService) { }

  //inputs
  @Input() productId: string;

  //component data
  positiveReviews: ReviewData[] = [];
  negativeReviews: ReviewData[] = [];
  newReview: CreateReviewData = new CreateReviewData();

  ngOnInit() {
    this.reviewService.getReviewsOfProduct(this.productId)
      .subscribe((reviews: ReviewData[]) => {  
        reviews.forEach(r => {        
          if (r.mood == ReviewMood.Positive){
            this.positiveReviews.push(r);
          }
          if (r.mood == ReviewMood.Negative){
            this.negativeReviews.push(r);
          }
        });
      })
  }
}
