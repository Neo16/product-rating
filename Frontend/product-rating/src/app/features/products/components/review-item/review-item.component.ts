import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
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
  @Output() upvote: EventEmitter<string> = new EventEmitter();
  @Output() downvote: EventEmitter<string> = new EventEmitter();

  ngOnInit() {
  }

  callUpvote(reviewId: string){
    this.upvote.emit(reviewId);
  }
  callDownvote(reviewId: string){
    this.downvote.emit(reviewId);
  }

}
