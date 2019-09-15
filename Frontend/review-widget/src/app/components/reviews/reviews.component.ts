import { Component, OnInit, Input, ViewChild, ElementRef } from '@angular/core';
import { ReviewEditArgs } from '../review-item/review-item.component';
import { ReviewService } from '../../services/review-service';
import { ReviewData } from '../../models/ReviewData';
import { CreateReviewData } from '../../models/CreateReviewData';
import { ReviewMood, ReviewMoodDisplay } from '../../models/ReviewMood';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { LoginService } from 'src/app/services/login-service';
import { LoginFormComponent } from '../login-form/login-form.component';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.scss']
})
export class ReviewsComponent implements OnInit {

  //template refs
  //@ViewChild('loginModal', { static: false }) loginModal: ElementRef;

  //inputs
  @Input() productid: string;
  @Input() key: string;

  //component data
  positiveReviews: ReviewData[] = [];
  negativeReviews: ReviewData[] = [];
  newReview: CreateReviewData = new CreateReviewData();
  reviewMood = ReviewMood;
  moodDisplay = ReviewMoodDisplay;
  isLoggedIn: boolean = false;
  showForm: boolean = false;

  constructor(   
    private loginService: LoginService,
    private reviewService: ReviewService,
    private modalService: NgbModal
  ) {}

  ngOnInit() {
    this.loginService.setApiKey(this.key);
    this.reviewService.setApiKey(this.key);    
    this.checkIfLoggedIn();   

    this.newReview.mood = ReviewMood.Positive;
    this.newReview.productId = this.productid;
    this.reviewService.getReviewsOfProduct(this.productid)
      .subscribe((reviews: ReviewData[]) => {
        reviews.forEach(r => {
          this.loadReviewToPage(r);
        });
      })
  }

  loadReviewToPage(r: ReviewData, beFirst: boolean = false) {
    if (r.mood == ReviewMood.Positive) {
      beFirst ? this.positiveReviews.unshift(r) : this.positiveReviews.push(r);
    }
    if (r.mood == ReviewMood.Negative) {
      beFirst ? this.negativeReviews.unshift(r) : this.negativeReviews.push(r);
    }
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
    if (!this.isLoggedIn){
      this.openLoginPopup();
    }   
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
    if (!this.isLoggedIn){
      this.openLoginPopup();
    }   
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

  openLoginPopup() {
    this.modalService.open(LoginFormComponent).result.then((result) => {    
      this.checkIfLoggedIn();
    }, (reason) => {     
      this.checkIfLoggedIn();
    });
  }

  findReview(reviewId: string) {
    return this.positiveReviews.concat(this.negativeReviews)
      .find(x => x.id === reviewId);
  }

  checkIfLoggedIn() {
    var userToken = localStorage.getItem('productrating-token');
    this.isLoggedIn = userToken != null;  
  } 

  logOut(){
    localStorage.removeItem('productrating-token');
    this.isLoggedIn = false;
  }
}
