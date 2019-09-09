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

  pressedUpvote(){  
    if (this.review.wasUpvotedByMe === true)
    {  
       this.downvote.emit(this.review.id); 
       return;
    }    
    this.upvote.emit(this.review.id);
  }
  pressedDownvote(){
    if (this.review.wasDownvotedByMe === true)
    {       
        this.upvote.emit(this.review.id);
        return;
    }    
    this.downvote.emit(this.review.id);
  }
}
