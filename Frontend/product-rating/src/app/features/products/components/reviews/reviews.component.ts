import { Component, OnInit, Input } from '@angular/core';
import { ReviewData } from 'src/app/models/reviews/ReviewData';
import { CreateReviewData } from 'src/app/models/reviews/CreateReviewData';
import { ReviewService } from '../../services/review.service';
import { ReviewMood, ReviewMoodDisplay } from 'src/app/models/reviews/ReviewMood';
import { Observable } from 'rxjs';
import { AccountState } from 'src/app/store/account-store/account.state';
import { Store } from '@ngrx/store';
import { selectAccountState } from 'src/app/store/root-state';
import { Router } from '@angular/router';
import { ReviewEditArgs } from '../review-item/review-item.component';
import { ModalService } from 'src/app/shared/services/modal-service';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.scss']
})
export class ReviewsComponent implements OnInit {

  constructor(
    private reviewService: ReviewService,
    private acountStore: Store<AccountState>,
    private router: Router,
    private modalService: ModalService
  ) {
    this.getAccountState = this.acountStore.select(selectAccountState);
  }

  //stores
  getAccountState: Observable<AccountState>;

  //inputs
  @Input() productId: string;

  //component data
  positiveReviews: ReviewData[] = [];
  negativeReviews: ReviewData[] = [];
  newReview: CreateReviewData = new CreateReviewData();
  reviewMood = ReviewMood;
  moodDisplay = ReviewMoodDisplay;
  isLoggedIn: boolean;
  showForm: boolean = false;
  userRoles: string[] = [];

  ngOnInit() {
    this.newReview.mood = ReviewMood.Positive;
    this.newReview.productId = this.productId;
    this.reviewService.getReviewsOfProduct(this.productId)
      .subscribe((reviews: ReviewData[]) => {
        reviews.forEach(r => {         
          this.loadReviewToPage(r);
        });
      })

    this.getAccountState.subscribe((accountState) => {
      this.isLoggedIn = accountState.isAuthenticated;
      if (this.isLoggedIn) {
        this.userRoles = accountState.user.roles;
      }
    });
  }

  loadReviewToPage(r: ReviewData, beFirst: boolean = false) {
    if (r.mood == ReviewMood.Positive) {
      beFirst ? this.positiveReviews.unshift(r) : this.positiveReviews.push(r);
    }
    if (r.mood == ReviewMood.Negative) {
      beFirst ? this.negativeReviews.unshift(r) : this.negativeReviews.push(r);
    }
  }

  cancelReview(){
    this.showForm = false;
    this.newReview = new CreateReviewData();
  }

  addReview() {
    this.reviewService.addNewReview(this.newReview)
      .subscribe((r: ReviewData) => {
        this.loadReviewToPage(r, true);
      })
    this.newReview.text = null;
    this.showForm = false;
  }

  upvote(reviewId: string) {
    this.redirectIfNotLoggedIn();
    var review = this.positiveReviews.concat(this.negativeReviews)
      .find(x => x.id === reviewId);

    this.reviewService.upvoteReview(reviewId)
      .subscribe(e => {
        review.points++;
        review.wasUpvotedByMe = !review.wasDownvotedByMe;
        review.wasDownvotedByMe = false;
      })
  }

  downvote(reviewId: string) {
    this.redirectIfNotLoggedIn();
    var review = this.findReview(reviewId);

    this.reviewService.downvoteReview(reviewId)
      .subscribe(e => {
        review.points--;
        review.wasDownvotedByMe = !review.wasUpvotedByMe;
        review.wasUpvotedByMe = false;
      })
  }

  edit(args: ReviewEditArgs) {

    var review = this.findReview(args.reviewId);

    this.reviewService.editReview(args.reviewId, args.reviewText, args.mood)
      .subscribe(e => {
        review.mood = args.mood;
        review.text = args.reviewText;
      });
  }
  delete(reviewId: string) {
    var review = this.findReview(reviewId);

    this.reviewService.deleteReview(reviewId)
      .subscribe(e => {
        this.positiveReviews = this.positiveReviews.filter(r => r.id != reviewId);
        this.negativeReviews = this.negativeReviews.filter(r => r.id != reviewId);
      });
  }
  redirectIfNotLoggedIn() {
    if (!this.isLoggedIn) {
      this.router.navigate(['/account/login'], { queryParams: { return: this.router.url } });
    }
  }

  findReview(reviewId: string) {
    return this.positiveReviews.concat(this.negativeReviews)
      .find(x => x.id === reviewId);
  }

  adminDelete(review: ReviewData) {
    //Tell if user has admin rights 
    if (this.userRoles.some(e => e == "ADMIN")) {
      review.highLighted = true;
      this.modalService.openConfirmationModal("Confirm delete", "Are you sure, you want to delete this review?")
        .then(yep => {
          this.delete(review.id);
          review.highLighted = false;
        }, nope => { review.highLighted = false; })
    }
  }
}
