import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ReviewData } from '../../models/ReviewData';
import { ReviewMood } from '../../models/ReviewMood';


@Component({
  selector: 'app-review-item',
  templateUrl: './review-item.component.html',
  styleUrls: ['./review-item.component.scss']
})
export class ReviewItemComponent implements OnInit {

  constructor() { }

  @Input() review: ReviewData;
  @Output() upvoteEvent: EventEmitter<string> = new EventEmitter();
  @Output() downvoteEvent: EventEmitter<string> = new EventEmitter();
  @Output() editEvent: EventEmitter<ReviewEditArgs> = new EventEmitter<ReviewEditArgs>();
  @Output() deleteEvent: EventEmitter<string> = new EventEmitter<string>();
  isEditing: boolean = false;
  editedText: string;

  ngOnInit() {
    this.editedText = this.review.text;
  }

  pressedUpvote() {
    if (this.review.wasUpvotedByMe === true) {
      this.downvoteEvent.emit(this.review.id);
      return;
    }
    this.upvoteEvent.emit(this.review.id);
  }
  pressedDownvote() {
    if (this.review.wasDownvotedByMe === true) {
      this.upvoteEvent.emit(this.review.id);
      return;
    }
    this.downvoteEvent.emit(this.review.id);
  }
  edit() {
    this.isEditing = true;    
  }
  save() {    
    this.isEditing = false;
    this.editEvent.emit({
      reviewId: this.review.id,
      reviewText: this.editedText,
      mood: this.review.mood
    });    
  }
  delete() {
    this.deleteEvent.emit(this.review.id);
  }
}

export class ReviewEditArgs {
  reviewId: string;
  reviewText: string;
  mood: ReviewMood;
}
